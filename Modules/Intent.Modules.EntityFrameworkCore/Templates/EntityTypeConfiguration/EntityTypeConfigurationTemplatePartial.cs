using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Intent.Engine;
using Intent.Metadata.Models;
using Intent.Metadata.RDBMS.Api;
using Intent.Modelers.Domain.Api;
using Intent.Modules.Common;
using Intent.Modules.Common.CSharp.Templates;
using Intent.Modules.Common.CSharp.VisualStudio;
using Intent.Modules.Common.Templates;
using Intent.Modules.EntityFrameworkCore.Settings;
using Intent.Modules.Metadata.RDBMS.Api.Indexes;
using Intent.Modules.Metadata.RDBMS.Settings;
using Intent.RoslynWeaver.Attributes;
using Intent.Templates;
using Intent.Utils;

[assembly: DefaultIntentManaged(Mode.Merge)]
[assembly: IntentTemplate("Intent.ModuleBuilder.CSharp.Templates.CSharpTemplatePartial", Version = "1.0")]

namespace Intent.Modules.EntityFrameworkCore.Templates.EntityTypeConfiguration
{
    public class EntityTypeConfigurationCreatedEvent
    {
        public EntityTypeConfigurationCreatedEvent(EntityTypeConfigurationTemplate template)
        {
            Template = template;
        }

        public EntityTypeConfigurationTemplate Template { get; set; }
    }

    [IntentManaged(Mode.Merge, Signature = Mode.Fully)]
    partial class EntityTypeConfigurationTemplate : CSharpTemplateBase<ClassModel, EntityTypeConfigurationDecorator>
    {
        //private readonly List<AttributeModel> _explicitPrimaryKeys;
        private readonly List<string> _ownedTypeConfigMethods = new List<string>();

        [IntentManaged(Mode.Fully)]
        public const string TemplateId = "Intent.EntityFrameworkCore.EntityTypeConfiguration";

        [IntentManaged(Mode.Merge, Signature = Mode.Fully)]
        public EntityTypeConfigurationTemplate(IOutputTarget outputTarget, ClassModel model) : base(TemplateId, outputTarget, model)
        {
            AddNugetDependency(NugetPackages.EntityFrameworkCore(Project));
            AddTypeSource("Domain.Entity");
            AddTypeSource("Domain.ValueObject");
        }

        public override void BeforeTemplateExecution()
        {
            ExecutionContext.EventDispatcher.Publish(new EntityTypeConfigurationCreatedEvent(this));
        }

        [IntentManaged(Mode.Fully, Body = Mode.Ignore)]
        protected override CSharpFileConfig DefineFileConfig()
        {
            return new CSharpFileConfig(
                className: $"{Model.Name}Configuration",
                @namespace: $"{OutputTarget.GetNamespace()}");
        }

        private string GetEntityName()
        {
            return GetTypeName("Domain.Entity", Model);
        }

        private string GetTableMapping(ClassModel model)
        {
            if (model.ParentClass != null && ExecutionContext.Settings.GetDatabaseSettings().InheritanceStrategy()
                    .IsTablePerHierarchy())
            {
                return $@"
            builder.HasBaseType<{GetTypeName("Domain.Entity", model.ParentClass)}>();
";
            }

            if (model.HasTable() || !ExecutionContext.Settings.GetDatabaseSettings().InheritanceStrategy()
                    .IsTablePerHierarchy())
            {
                return $@"
            builder.ToTable(""{model.GetTable()?.Name() ?? model.Name}""{(!string.IsNullOrWhiteSpace(model.GetTable()?.Schema()) ? @$", ""{model.GetTable().Schema() ?? "dbo"}""" : "")});
";
            }

            return string.Empty;
        }

        private string GetKeyMapping(ClassModel model)
        {
            if (model.ParentClass != null && (!model.ParentClass.IsAbstract || !ExecutionContext.Settings
                    .GetDatabaseSettings().InheritanceStrategy().IsTablePerConcreteType()))
            {
                return string.Empty;
            }

            if (!model.GetExplicitPrimaryKey().Any())
            {
                if (IsValueObject(model.InternalElement))
                {
                    return "";
                }
                return $@"
                builder.HasKey(x => x.Id);";
                //    return $@"
                //builder.HasKey(x => x.Id);
                //builder.Property(x => x.Id)
                //       .UsePropertyAccessMode(PropertyAccessMode.Property)
                //       .ValueGeneratedNever();";
            }
            else
            {
                var keys = model.GetExplicitPrimaryKey().Count() == 1
                    ? "x." + model.GetExplicitPrimaryKey().Single().Name.ToPascalCase()
                    : $"new {{ {string.Join(", ", model.GetExplicitPrimaryKey().Select(x => "x." + x.Name.ToPascalCase()))} }}";
                return $@"
            builder.HasKey(x => {keys});";
            }
        }

        private bool RequiresConfiguration(AttributeModel attribute)
        {
            return Model.GetExplicitPrimaryKey().All(key => !key.Equals(attribute)) &&
                   !attribute.Name.Equals("id", StringComparison.InvariantCultureIgnoreCase);
        }

        private bool RequiresConfiguration(AssociationEndModel associationEnd)
        {
            return associationEnd.IsTargetEnd();
        }

        private string GetAttributeMapping(AttributeModel attribute)
        {
            var statements = new List<string>();

            if (!IsValueObject(attribute.TypeReference.Element))
            {
                statements.Add($"builder.Property(x => x.{attribute.Name.ToPascalCase()})");
                statements.AddRange(GetAttributeMappingStatements(attribute));

                return $@"
            {string.Join(@"
                ", statements)};";

            }

            _ownedTypeConfigMethods.Add(@$"
        public void Configure{attribute.Name.ToPascalCase()}(OwnedNavigationBuilder<{GetTypeName(attribute.InternalElement.ParentElement, null)}, {GetTypeName((IElement)attribute.TypeReference.Element, null)}> builder)
        {{{
            string.Join(@"
            ", GetTypeConfiguration((IElement)attribute.TypeReference.Element))}
        }}");
            return $"builder.OwnsOne(x => x.{attribute.Name.ToPascalCase()}, Configure{attribute.Name.ToPascalCase()});";
        }

        private List<string> GetAttributeMappingStatements(AttributeModel attribute)
        {
            var statements = new List<string>();
            if (!attribute.Type.IsNullable)
            {
                statements.Add(".IsRequired()");
            }

            if (attribute.GetPrimaryKey()?.Identity() == true)
            {
                statements.Add(".UseSqlServerIdentityColumn()");
            }

            if (attribute.HasDefaultConstraint())
            {
                var treatAsSqlExpression = attribute.GetDefaultConstraint().TreatAsSQLExpression();
                var defaultValue = attribute.GetDefaultConstraint()?.Value() ?? string.Empty;

                if (!treatAsSqlExpression &&
                    !defaultValue.TrimStart().StartsWith("\"") &&
                    attribute.Type.Element.Name == "string")
                {
                    defaultValue = $"\"{defaultValue}\"";
                }

                var method = treatAsSqlExpression
                    ? "HasDefaultValueSql"
                    : "HasDefaultValue";

                statements.Add($".{method}({defaultValue})");
            }

            if (attribute.GetTextConstraints()?.SQLDataType().IsDEFAULT() == true)
            {
                var maxLength = attribute.GetTextConstraints().MaxLength();
                if (maxLength.HasValue && attribute.Type.Element.Name == "string")
                {
                    statements.Add($".HasMaxLength({maxLength.Value})");
                }

                //var isUnicode = attribute.GetTextConstraints().SQLDataType().IsVARCHAR() == true;
                //if (isUnicode)
                //{
                //    statements.Add($".IsUnicode(false)");
                //}
            }
            else if (attribute.HasTextConstraints())
            {
                var maxLength = attribute.GetTextConstraints().MaxLength();
                switch (attribute.GetTextConstraints().SQLDataType().AsEnum())
                {
                    case AttributeModelStereotypeExtensions.TextConstraints.SQLDataTypeOptionsEnum.VARCHAR:
                        statements.Add($".HasColumnType(\"varchar({maxLength?.ToString() ?? "max"})\")");
                        break;
                    case AttributeModelStereotypeExtensions.TextConstraints.SQLDataTypeOptionsEnum.NVARCHAR:
                        statements.Add($".HasColumnType(\"nvarchar({maxLength?.ToString() ?? "max"})\")");
                        break;
                    case AttributeModelStereotypeExtensions.TextConstraints.SQLDataTypeOptionsEnum.TEXT:
                        Logging.Log.Warning(
                            $"{Model.Name}.{attribute.Name}: The ntext, text, and image data types will be removed in a future version of SQL Server. Avoid using these data types in new development work, and plan to modify applications that currently use them. Use nvarchar(max), varchar(max), and varbinary(max) instead.");
                        statements.Add($".HasColumnType(\"text\")");
                        break;
                    case AttributeModelStereotypeExtensions.TextConstraints.SQLDataTypeOptionsEnum.NTEXT:
                        Logging.Log.Warning(
                            $"{Model.Name}.{attribute.Name}: The ntext, text, and image data types will be removed in a future version of SQL Server. Avoid using these data types in new development work, and plan to modify applications that currently use them. Use nvarchar(max), varchar(max), and varbinary(max) instead.");
                        statements.Add($".HasColumnType(\"ntext\")");
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            else
            {
                var decimalPrecision = attribute.GetDecimalConstraints()?.Precision();
                var decimalScale = attribute.GetDecimalConstraints()?.Scale();
                var columnType = attribute.GetColumn()?.Type();
                if (decimalPrecision.HasValue && decimalScale.HasValue)
                {
                    statements.Add($".HasColumnType(\"decimal({decimalPrecision}, {decimalScale})\")");
                }
                else if (!string.IsNullOrWhiteSpace(columnType))
                {
                    statements.Add($".HasColumnType(\"{columnType}\")");
                }
            }

            var columnName = attribute.GetColumn()?.Name();
            if (!string.IsNullOrWhiteSpace(columnName))
            {
                statements.Add($".HasColumnName(\"{columnName}\")");
            }

            var computedValueSql = attribute.GetComputedValue()?.SQL();
            if (!string.IsNullOrWhiteSpace(computedValueSql))
            {
                statements.Add(
                    $".HasComputedColumnSql(\"{computedValueSql}\"{(attribute.GetComputedValue().Stored() ? ", stored: true" : string.Empty)})");
            }

            if (Intent.EntityFrameworkCore.Api.AttributeModelStereotypeExtensions.HasRowVersion(attribute))
            {
                statements.Add($".IsRowVersion()");
            }

            return statements;
        }

        private string GetAssociationMapping(AssociationEndModel associationEnd)
        {
            var statements = new List<string>();

            if (associationEnd.Element.Id.Equals(associationEnd.OtherEnd().Element.Id)
                && associationEnd.Name.Equals(associationEnd.Element.Name))
            {
                Logging.Log.Warning(
                    $"Self referencing relationship detected using the same name for the Association as the Class: {associationEnd.Class.Name}. This might cause problems.");
            }

            switch (associationEnd.Association.GetRelationshipType())
            {
                case RelationshipType.OneToOne:
                    if (IsValueObject(associationEnd.Element))
                    {
                        _ownedTypeConfigMethods.Add(@$"
        public void Configure{associationEnd.Name.ToPascalCase()}(OwnedNavigationBuilder<{GetTypeName((IElement)associationEnd.OtherEnd().Element, null)}, {GetTypeName((IElement)associationEnd.Element, null)}> builder)
        {{
            builder.WithOwner({(associationEnd.OtherEnd().IsNavigable ? $"x => x.{associationEnd.OtherEnd().Name.ToPascalCase()}" : "")}).HasForeignKey(x => x.Id);{
            string.Join(@"
            ", GetTypeConfiguration((IElement)associationEnd.Element))}
        }}");
                        return $@"
            builder.OwnsOne(x => x.{associationEnd.Name.ToPascalCase()}, Configure{associationEnd.Name.ToPascalCase()});";
                    }

                    statements.Add($"builder.HasOne(x => x.{associationEnd.Name.ToPascalCase()})");
                    statements.Add(
                        $".WithOne({(associationEnd.OtherEnd().IsNavigable ? $"x => x.{associationEnd.OtherEnd().Name.ToPascalCase()}" : "")})");

                    if (associationEnd.IsNullable)
                    {
                        if (associationEnd.OtherEnd().IsNullable)
                        {
                            statements.Add(
                                $".HasForeignKey<{associationEnd.OtherEnd().Class.Name}>(x => x.{associationEnd.Name.ToPascalCase()}Id)");
                        }
                        else
                        {
                            statements.Add($".HasForeignKey<{associationEnd.Class.Name}>(x => x.Id)");
                        }
                    }
                    else
                    {
                        statements.Add($".HasForeignKey<{Model.Name}>(x => x.Id)");
                    }

                    if (!associationEnd.OtherEnd().IsNullable)
                    {
                        statements.Add($".IsRequired()");
                        statements.Add($".OnDelete(DeleteBehavior.Cascade)");
                    }
                    else
                    {
                        statements.Add($".OnDelete(DeleteBehavior.Restrict)");
                    }

                    break;
                case RelationshipType.ManyToOne:
                    statements.Add($"builder.HasOne(x => x.{associationEnd.Name.ToPascalCase()})");
                    statements.Add(
                        $".WithMany({(associationEnd.OtherEnd().IsNavigable ? "x => x." + associationEnd.OtherEnd().Name.ToPascalCase() : "")})");
                    statements.Add($".HasForeignKey({GetForeignKeyLambda(associationEnd.OtherEnd())})");
                    statements.Add($".OnDelete(DeleteBehavior.Restrict)");
                    break;
                case RelationshipType.OneToMany:
                    if (IsValueObject(associationEnd.Element))
                    {
                        _ownedTypeConfigMethods.Add(@$"
        public void Configure{associationEnd.Name.ToPascalCase()}(OwnedNavigationBuilder<{GetTypeName((IElement)associationEnd.OtherEnd().Element, null)}, {GetTypeName((IElement)associationEnd.Element, null)}> builder)
        {{
            builder.WithOwner({(associationEnd.OtherEnd().IsNavigable ? $"x => x.{associationEnd.OtherEnd().Name.ToPascalCase()}" : "")}).HasForeignKey({GetForeignKeyLambda(associationEnd)});{
            string.Join(@"
            ", GetTypeConfiguration((IElement)associationEnd.Element))}
        }}");
                        return $@"
            builder.OwnsMany(x => x.{associationEnd.Name.ToPascalCase()}, Configure{associationEnd.Name.ToPascalCase()});";
                    }
                    statements.Add($"builder.HasMany(x => x.{associationEnd.Name.ToPascalCase()})");
                    statements.Add(
                        $".WithOne({(associationEnd.OtherEnd().IsNavigable ? $"x => x.{associationEnd.OtherEnd().Name.ToPascalCase()}" : $"")})");
                    statements.Add($".HasForeignKey({GetForeignKeyLambda(associationEnd)})");
                    if (!associationEnd.OtherEnd().IsNullable)
                    {
                        statements.Add($".IsRequired()");
                        statements.Add($".OnDelete(DeleteBehavior.Cascade)");
                    }

                    break;
                case RelationshipType.ManyToMany:
                    if (Project.GetProject().IsNetCore2App || Project.GetProject().IsNetCore3App)
                    {
                        statements.Add($"builder.Ignore(x => x.{associationEnd.Name.ToPascalCase()})");

                        Logging.Log.Warning(
                            $@"Intent.EntityFrameworkCore: Cannot create mapping relationship from {Model.Name} to {associationEnd.Class.Name}. It has been ignored, and will not be persisted.
    Many-to-Many relationships are not yet supported by EntityFrameworkCore as yet.
    Either upgrade your solution to .NET 5.0 or create a joining-table entity (e.g. [{Model.Name}] 1 --> * [{Model.Name}{associationEnd.Class.Name}] * --> 1 [{associationEnd.Class.Name}])
    For more information, please see https://github.com/aspnet/EntityFrameworkCore/issues/1368");
                    }
                    else // if .NET 5.0 or above...
                    {
                        statements.Add($"builder.HasMany(x => x.{associationEnd.Name.ToPascalCase()})");
                        statements.Add(
                            $".WithMany({(associationEnd.OtherEnd().IsNavigable ? $"x => x.{associationEnd.OtherEnd().Name.ToPascalCase()}" : $"\"{associationEnd.OtherEnd().Name.ToPascalCase()}\"")})");
                        statements.Add(
                            $".UsingEntity(x => x.ToTable(\"{associationEnd.OtherEnd().Class.Name}{associationEnd.Class.Name.ToPluralName()}\"))");
                    }

                    break;
                default:
                    throw new Exception(
                        $"Relationship type for association [{Model.Name}.{associationEnd.Name}] could not be determined.");
            }

            return $@"
            {string.Join(@"
                ", statements)};";
        }

        private List<string> GetTypeConfiguration(IElement targetType)
        {
            var statements = new List<string>();
            var attributes = targetType.ChildElements
                .Where(x => x.IsAttributeModel())
                .Select(x => x.AsAttributeModel())
                .ToList();

            var associations = targetType.AssociatedElements
                .Where(x => x.IsAssociationEndModel())
                .Select(x => x.AsAssociationEndModel())
                .ToList();

            if (targetType.IsClassModel())
            {
                statements.Add(GetTableMapping(targetType.AsClassModel()));
                if (targetType.AsClassModel().GetExplicitPrimaryKey().Any())
                {
                    statements.Add(GetKeyMapping(targetType.AsClassModel()));
                }
            }
            statements.AddRange(attributes.Where(RequiresConfiguration).Select(GetAttributeMapping));
            statements.AddRange(associations.Where(RequiresConfiguration).Select(GetAssociationMapping));
            if (targetType.IsClassModel())
            {
                statements.AddRange(GetIndexes(targetType.AsClassModel()));
            }

            return statements;
        }

        private string GetCheckConstraints()
        {
            var checkConstraints = Model.GetCheckConstraints();
            if (checkConstraints.Count == 0)
            {
                return string.Empty;
            }

            var sb = new StringBuilder(@"
            builder");

            foreach (var checkConstraint in checkConstraints)
            {
                sb.Append(@$"
                .HasCheckConstraint(""{checkConstraint.Name()}"", ""{checkConstraint.SQL()}"")");
            }

            sb.Append(";");
            return sb.ToString();
        }

        private List<string> GetIndexes(ClassModel model)
        {
            var indexes = model.GetIndexes();
            if (indexes.Count == 0)
            {
                return new List<string>();
            }

            var statements = new List<string>();

            foreach (var index in indexes)
            {
                var indexFields = index.KeyColumns.Length == 1
                    ? GetIndexColumnPropertyName(index.KeyColumns.Single(), "x.")
                    : $"new {{ {string.Join(", ", index.KeyColumns.Select(x => GetIndexColumnPropertyName(x, "x.")))} }}";

                var sb = new StringBuilder($@"
            builder.HasIndex(x => {indexFields})");

                if (index.IncludedColumns.Length > 0)
                {
                    sb.Append($@"
                .IncludeProperties(x => new {{ {string.Join(", ", index.IncludedColumns.Select(x => GetIndexColumnPropertyName(x, "x.")))} }})");
                }

                switch (index.FilterOption)
                {
                    case FilterOption.Default:
                        break;
                    case FilterOption.None:
                        sb.Append(@"
                .HasFilter(null)");
                        break;
                    case FilterOption.Custom:
                        sb.Append(@$"
                .HasFilter(\""{index.Filter}"")");
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                if (index.IsUnique)
                {
                    sb.Append(@"
                .IsUnique()");
                }

                if (!index.UseDefaultName)
                {
                    sb.Append(@$"
                .HasDatabaseName(""{index.Name}"")");
                }

                sb.Append(";");

                statements.Add(sb.ToString());
            }

            return statements;
        }

        private bool IsValueObject(ICanBeReferencedType type)
        {
            if (type.IsClassModel())
            {
                return !type.AsClassModel().IsAggregateRoot();
            }
            return TryGetTypeName("Domain.ValueObject", type.Id,
                out var typename);
        }

        private static string GetIndexColumnPropertyName(IndexColumn column, string prefix = null)
        {
            return column.SourceType.IsAssociationEndModel()
                ? $"{prefix}{column.Name.ToPascalCase()}Id"
                : $"{prefix}{column.Name.ToPascalCase()}";
        }

        private static string GetForeignKeyLambda(AssociationEndModel associationEnd)
        {
            var columns = new List<string>();
            if (associationEnd.HasForeignKey() &&
                !string.IsNullOrWhiteSpace(associationEnd.GetForeignKey().ColumnName()))
            {
                columns.AddRange(associationEnd.GetForeignKey().ColumnName()
                    .Split(',') // upgrade stereotype to have a selection list.
                    .Select(x => x.Trim()));
            }
            else if (associationEnd.OtherEnd().Class.GetExplicitPrimaryKey().Any())
            {
                columns.AddRange(associationEnd.OtherEnd().Class.GetExplicitPrimaryKey().Select(x =>
                    $"{associationEnd.OtherEnd().Name.ToPascalCase()}{x.Name.ToPascalCase()}"));
            }
            else // implicit Id
            {
                columns.Add($"{associationEnd.OtherEnd().Name.ToPascalCase()}Id");
            }

            if (columns.Count == 1)
            {
                return $"x => x.{columns.Single()}";
            }

            return $"x => new {{ {string.Join(", ", columns.Select(x => "x." + x))}}}";
        }

        private IEnumerable<AttributeModel> GetAttributes(ClassModel model)
        {
            var attributes = new List<AttributeModel>();
            if (model.ParentClass != null && model.ParentClass.IsAbstract && ExecutionContext.Settings
                    .GetDatabaseSettings().InheritanceStrategy().IsTablePerConcreteType())
            {
                attributes.AddRange(GetAttributes(model.ParentClass));
            }

            attributes.AddRange(model.Attributes.Where(RequiresConfiguration).ToList());
            return attributes;
        }

        private IEnumerable<AssociationEndModel> GetAssociations(ClassModel model)
        {
            var associations = new List<AssociationEndModel>();
            if (model.ParentClass != null && model.ParentClass.IsAbstract && ExecutionContext.Settings
                    .GetDatabaseSettings().InheritanceStrategy().IsTablePerConcreteType())
            {
                associations.AddRange(GetAssociations(model.ParentClass));
            }

            associations.AddRange(model.AssociatedClasses.Where(RequiresConfiguration).ToList());
            return associations;
        }

        private string GetAdditionalMethods()
        {
            _ownedTypeConfigMethods.Reverse(); // methods are added to this list recursively and therefore the methods are in reverse dependency order.
            return _ownedTypeConfigMethods.Any() ? @$"
        {string.Join(@"
        ", _ownedTypeConfigMethods)}" : "";

        }
    }
}