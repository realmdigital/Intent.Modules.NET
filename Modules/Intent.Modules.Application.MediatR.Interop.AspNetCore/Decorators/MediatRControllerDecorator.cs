using System.Collections.Generic;
using System.Linq;
using System.Text;
using Intent.Modelers.Services.Api;
using Intent.Modelers.Services.CQRS.Api;
using Intent.Modules.Application.MediatR.Templates.CommandModels;
using Intent.Modules.Application.MediatR.Templates.DtoModel;
using Intent.Modules.Application.MediatR.Templates.QueryModels;
using Intent.Modules.AspNetCore.Controllers.Templates.Controller;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Ignore)]
[assembly: IntentTemplate("ModuleBuilder.Templates.TemplateDecorator", Version = "1.0")]

namespace Intent.Modules.Application.MediatR.Interop.AspNetCore.Decorators
{
    [IntentManaged(Mode.Merge)]
    public class MediatRControllerDecorator : ControllerDecorator
    {
        public const string DecoratorId = "Application.MediatR.Interop.AspNetCore.MediatRControllerDecorator";
        private readonly ControllerTemplate _template;

        public MediatRControllerDecorator(ControllerTemplate template)
        {
            _template = template;
            _template.AddTypeSource(CommandModelsTemplate.TemplateId);
            _template.AddTypeSource(QueryModelsTemplate.TemplateId);
            _template.AddTypeSource(DtoModelTemplate.TemplateId);
        }

        public override string[] DependencyNamespaces()
        {
            return new[]
            {
                "MediatR",
                "Microsoft.AspNetCore.Mvc",
                "Microsoft.Extensions.DependencyInjection"
            };
        }

        public override string EnterClass()
        {
            return @"
        private ISender _mediator;

        protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetService<ISender>();
";
        }

        public override string EnterOperationBody(OperationModel operationModel)
        {
            var validations = new StringBuilder();
            var payloadParameter = GetPayloadParameter(operationModel);
            if (payloadParameter != null && operationModel.InternalElement.IsMapped)
            {
                foreach (var mappedParameter in GetMappedParameters(operationModel))
                {
                    validations.Append($@"
            if ({mappedParameter.Name} != {payloadParameter.Name}.{mappedParameter.InternalElement.MappedElement.Element.Name})
            {{
                return BadRequest();
            }}
            ");
                }
            }

            return validations.ToString();
        }

        public override string ExitOperationBody(OperationModel operationModel)
        {
            var payload = GetPayloadParameter(operationModel)?.Name
                ?? (operationModel.InternalElement.IsMapped ? GetMappedPayload(operationModel) : "UNKNOWN");
            if (operationModel.ReturnType != null)
            {
                return $"return await Mediator.Send({payload});";
            }

            return $@"await Mediator.Send({payload});

            return NoContent();";
        }

        private ParameterModel GetPayloadParameter(OperationModel operationModel)
        {
            return operationModel.Parameters.SingleOrDefault(x =>
                x.Type.Element.SpecializationTypeId == CommandModel.SpecializationTypeId ||
                x.Type.Element.SpecializationTypeId == QueryModel.SpecializationTypeId);
        }

        public IList<ParameterModel> GetMappedParameters(OperationModel operationModel)
        {
            return operationModel.Parameters.Where(x => x.InternalElement.IsMapped).ToList();
        }

        private string GetMappedPayload(OperationModel operationModel)
        {
            var mappedElement = operationModel.InternalElement.MappedElement;
            if (GetMappedParameters(operationModel).Any())
            {
                return $"new {_template.GetTypeName(mappedElement)} {{ {string.Join(", ", GetMappedParameters(operationModel).Select(x => x.InternalElement.MappedElement.Element.Name + " = " + x.Name))}}}";
            }
            return $"new {_template.GetTypeName(mappedElement)}()";
        }

    }
}