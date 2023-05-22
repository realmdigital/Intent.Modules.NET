using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Intent.Engine;
using Intent.Modelers.Services.Api;
using Intent.Modules.Application.Dtos.Settings;
using Intent.Modules.Application.Dtos.Templates.ContractEnumModel;
using Intent.Modules.Common;
using Intent.Modules.Common.CSharp.Builder;
using Intent.Modules.Common.CSharp.Templates;
using Intent.Modules.Common.Templates;
using Intent.Modules.Common.VisualStudio;
using Intent.Modules.Constants;
using Intent.RoslynWeaver.Attributes;
using Intent.Templates;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.ModuleBuilder.CSharp.Templates.CSharpTemplatePartial", Version = "1.0")]

namespace Intent.Modules.Application.Dtos.Templates.DtoModel
{
    [IntentManaged(Mode.Fully, Body = Mode.Merge)]
    public partial class DtoModelTemplate : CSharpTemplateBase<DTOModel, DtoModelDecorator>, ICSharpFileBuilderTemplate
    {
        public const string TemplateId = "Intent.Application.Dtos.DtoModel";

        [IntentManaged(Mode.Fully, Body = Mode.Ignore)]
        public DtoModelTemplate(IOutputTarget outputTarget, DTOModel model) : base(TemplateId, outputTarget, model)
        {
            AddAssemblyReference(new GacAssemblyReference("System.Runtime.Serialization"));
            AddTypeSource(DtoModelTemplate.TemplateId, "List<{0}>");
            AddTypeSource(TemplateFulfillingRoles.Domain.Enum, "List<{0}>");
            FulfillsRole(TemplateFulfillingRoles.Application.Contracts.Dto);
            AddTypeSource(ContractEnumModelTemplate.TemplateId, "List<{0}>");

            var csharpFile = new CSharpFile(this.GetNamespace(), this.GetFolderPath());
            AddTypeDeclartion(csharpFile);
            csharpFile.OnBuild((Action<CSharpFile>)(file =>
                {
                    file.AddUsing("System");
                    file.AddUsing("System.Collections.Generic");
                    var @class = file.TypeDeclarations.First();

                    ConfigureClass(@class);

                    var enterClass = GetDecorators(d => d.EnterClass());
                    foreach (var line in enterClass)
                        @class.AddCodeBlock(line);

                    @class.AddConstructor();

                    if (!Model.IsAbstract)
                    {
                        @class.AddMethod(base.GetTypeName(this), "Create", method =>
                        {
                            method.Static();
                            foreach (var field in Model.Fields)
                            {
                                method.AddParameter(base.GetTypeName(field.TypeReference), field.Name.ToParameterName());
                            }
                            method.AddObjectInitializerBlock($"return new {base.GetTypeName(this)}", block =>
                            {
                                foreach (var field in Model.Fields)
                                {
                                    block.AddInitStatement(field.Name.ToPascalCase(), field.Name.ToParameterName());
                                }
                                block.WithSemicolon();
                            });
                        });
                    }

                    foreach (var field in Model.Fields)
                    {
                        @class.AddProperty(base.GetTypeName(field.TypeReference), field.Name.ToPascalCase(), property =>
                        {
                            property.TryAddXmlDocComments(field.InternalElement);
                            SetAccessLevel(property.Setter);
                            property.WithComments(field.GetXmlDocLines());
                            property.AddMetadata("model", field);
                            AddPropertyAttributes(property, field);
                            if (!string.IsNullOrWhiteSpace(field.Value))
                            {
                                property.WithInitialValue(field.Value);
                            }
                            else 
                            {
                                var typeInfo = GetTypeInfo(field.TypeReference);
                                if (!typeInfo.IsPrimitive && typeInfo.IsNullable == false)
                                {
                                    property.WithInitialValue("null!");
                                }
                            }
                        });
                    }

                    var exitClass = GetDecorators(d => d.ExitClass());
                    foreach (var line in exitClass)
                        @class.AddCodeBlock(line);
                }));
            CSharpFile = csharpFile;
        }

        private void SetAccessLevel(CSharpPropertyAccessor setter)
        {
            var setting = ExecutionContext.Settings.GetDTOSettings().PropertySetterAccessibility().AsEnum();
            switch (setting)
            {
                case DTOSettings.PropertySetterAccessibilityOptionsEnum.Init:
                    setter.Init();
                    break;
                case DTOSettings.PropertySetterAccessibilityOptionsEnum.Internal:
                    setter.Internal();
                    break;
                case DTOSettings.PropertySetterAccessibilityOptionsEnum.Private:
                    setter.Private();
                    break;
                case DTOSettings.PropertySetterAccessibilityOptionsEnum.Protected:
                    setter.Protected();
                    break;
                case DTOSettings.PropertySetterAccessibilityOptionsEnum.Public:
                default:
                    //This property is public so don't want to duplicate the accessor
                    //setter.Public();
                    break;
            }
        }

        private void AddTypeDeclartion(CSharpFile csharpFile)
        {
            var typeDeclarationSetting = ExecutionContext.Settings.GetDTOSettings().Type();
            if (typeDeclarationSetting.IsRecord())
            {
                csharpFile.AddRecord($"{Model.Name}");
            }
            else
            {
                csharpFile.AddClass($"{Model.Name}");
            }
        }

        private void AddPropertyAttributes(CSharpProperty property, DTOFieldModel field)
        {
            var attributes = GetDecorators(x => x.PropertyAttributes(Model, field));
            foreach (var attribute in attributes)
                property.AddAttribute(attribute);
            if (TryGetSerializedName(field, out var serializedName))
            {
                property.AddAttribute(UseType("System.Text.Json.Serialization.JsonPropertyName"), attribute =>
                {
                    attribute.AddArgument($"\"{serializedName}\"");
                });
            }
        }

        private bool TryGetSerializedName(DTOFieldModel field, out string serializedName)
        {
            if (field.HasStereotype("Serialization Settings"))
            {
                var serializationSettings = field.GetStereotype("Serialization Settings");
                var namingConvention = serializationSettings.GetProperty<string>("Naming Convention");
                serializedName = namingConvention == "Custom"
                    ? serializationSettings.GetProperty<string>("Custom Name")
                    : ApplyConvention(field.Name, namingConvention);
                return true;
            }

            if (Model.HasStereotype("Serialization Settings"))
            {
                var namingConvention = Model.GetStereotype("Serialization Settings").GetProperty<string>("Field Naming Convention");
                serializedName = ApplyConvention(field.Name, namingConvention);
                return true;
            }

            serializedName = null;
            return false;
        }

        private static string ApplyConvention(string name, string convention)
        {
            return convention switch
            {
                "Camel Case" => name.ToCamelCase(),
                "Pascal Case" => name.ToPascalCase(),
                "Snake Case" => name.ToSnakeCase(),
                "Kebab Case" => name.ToKebabCase(),
                _ => throw new ArgumentOutOfRangeException(nameof(convention), $"{convention} is not a valid casing convention"),
            };
        }

        private void ConfigureClass(CSharpClass @class)
        {
            bool isTypeSealed = ExecutionContext.Settings.GetDTOSettings().Sealed();
            @class.AddMetadata("model", Model);
            if (Model.IsAbstract)
            {
                @class.Abstract();
            }
            if (isTypeSealed)
            {
                @class.Sealed();
            }
            @class.TryAddXmlDocComments(Model.InternalElement);
            if (Model.GenericTypes.Any())
            {
                foreach (var genericType in Model.GenericTypes)
                {
                    @class.AddGenericParameter(genericType);
                }
            }
            if (Model.ParentDtoTypeReference != null)
            {
                @class.WithBaseType(GetTypeName(Model.ParentDtoTypeReference));
                if (isTypeSealed)
                {
                    if (TryGetTemplate<ICSharpFileBuilderTemplate>(TemplateFulfillingRoles.Application.Contracts.Dto, Model.ParentDtoTypeReference.Element, out var template))
                    {
                        var parentClass = template.CSharpFile.TypeDeclarations.First();
                        parentClass.Unsealed();
                    }
                }
            }
            else if (GetDecorators().Any(x => !string.IsNullOrWhiteSpace(x.BaseClass())))
            {
                @class.WithBaseType(GetDecorators().FirstOrDefault(x => !string.IsNullOrWhiteSpace(x.BaseClass()))?.BaseClass());
            }

            @class.ImplementsInterfaces(GetDecorators().SelectMany(x => x.BaseInterfaces()));

            @class.XmlComments.AddStatements(Model.GetXmlDocLines());

            var attributes = GetDecorators(x => x.ClassAttributes(Model));
            foreach (var attr in attributes)
            {
                @class.AddAttribute(attr);
            }
        }

        private List<string> GetDecorators(Func<DtoModelDecorator, string> decoratorAction)
        {
            return GetDecorators()
                    .Select(x => decoratorAction(x))
                    .Where(x => x != null && x.Trim() != string.Empty)
                    .ToList();
        }

        [IntentManaged(Mode.Fully)]
        public CSharpFile CSharpFile { get; }

        [IntentManaged(Mode.Fully)]
        protected override CSharpFileConfig DefineFileConfig()
        {
            return CSharpFile.GetConfig();
        }

        [IntentManaged(Mode.Fully)]
        public override string TransformText()
        {
            return CSharpFile.ToString();
        }
    }
}