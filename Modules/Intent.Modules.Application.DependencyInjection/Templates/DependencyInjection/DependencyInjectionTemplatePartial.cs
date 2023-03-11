using System;
using System.Collections.Generic;
using System.Linq;
using Intent.Engine;
using Intent.Modules.Common;
using Intent.Modules.Common.CSharp.DependencyInjection;
using Intent.Modules.Common.CSharp.Templates;
using Intent.Modules.Common.Templates;
using Intent.RoslynWeaver.Attributes;
using Intent.Templates;

[assembly: DefaultIntentManaged(Mode.Merge)]
[assembly: IntentTemplate("Intent.ModuleBuilder.CSharp.Templates.CSharpTemplatePartial", Version = "1.0")]

namespace Intent.Modules.Application.DependencyInjection.Templates.DependencyInjection
{
    [IntentManaged(Mode.Merge, Signature = Mode.Fully)]
    partial class DependencyInjectionTemplate : CSharpTemplateBase<object, DependencyInjectionDecorator>
    {
        [IntentManaged(Mode.Fully)]
        public const string TemplateId = "Intent.Application.DependencyInjection.DependencyInjection";

        private readonly IList<ContainerRegistrationRequest> _containerRegistrationRequests = new List<ContainerRegistrationRequest>();
        private readonly IList<ServiceConfigurationRequest> _serviceConfigurationRequests = new List<ServiceConfigurationRequest>();

        [IntentManaged(Mode.Merge, Signature = Mode.Fully)]
        public DependencyInjectionTemplate(IOutputTarget outputTarget, object model = null) : base(TemplateId, outputTarget, model)
        {
            ExecutionContext.EventDispatcher.Subscribe<ContainerRegistrationRequest>(HandleEvent);
            ExecutionContext.EventDispatcher.Subscribe<ServiceConfigurationRequest>(HandleEvent);
        }

        public override void BeforeTemplateExecution()
        {
            ExecutionContext.EventDispatcher.Publish(
                ServiceConfigurationRequest
                    .ToRegister(
                        "AddApplication")
                    // Do we want to have Configuration as part of the Application registration?
                    // , ServiceConfigurationRequest.ParameterType.Configuration)
                    .HasDependency(this));
        }

        private void HandleEvent(ContainerRegistrationRequest @event)
        {
            if (@event.Concern != "Application")
            {
                return;
            }

            @event.MarkAsHandled();
            _containerRegistrationRequests.Add(@event);

            foreach (var templateDependency in @event.TemplateDependencies)
            {
                var template = GetTemplate<IClassProvider>(templateDependency);
                if (template != null)
                {
                    AddUsing(template.Namespace);
                }

                AddTemplateDependency(templateDependency);
            }

            foreach (var ns in @event.RequiredNamespaces)
            {
                AddUsing(ns);
            }
        }

        private void HandleEvent(ServiceConfigurationRequest @event)
        {
            if (@event.Concern != "Application")
            {
                return;
            }

            @event.MarkAsHandled();
            _serviceConfigurationRequests.Add(@event);

            foreach (var templateDependency in @event.TemplateDependencies)
            {
                var template = GetTemplate<IClassProvider>(templateDependency);
                if (template != null)
                {
                    AddUsing(template.Namespace);
                }

                AddTemplateDependency(templateDependency);
            }

            foreach (var ns in @event.RequiredNamespaces)
            {
                AddUsing(ns);
            }
        }

        [IntentManaged(Mode.Fully, Body = Mode.Ignore)]
        protected override CSharpFileConfig DefineFileConfig()
        {
            return new CSharpFileConfig(
                className: $"DependencyInjection",
                @namespace: $"{OutputTarget.GetNamespace()}");
        }

        private string DefineServiceRegistration(ContainerRegistrationRequest request)
        {
            string UseTypeOf(string type)
            {
                var typeName = type.Substring("typeof(".Length, type.Length - "typeof()".Length);
                return $"typeof({UseType(typeName)})";
            }

            var registrationType = request.Lifetime switch
            {
                ContainerRegistrationRequest.LifeTime.Singleton => "AddSingleton",
                ContainerRegistrationRequest.LifeTime.PerServiceCall => "AddScoped",
                ContainerRegistrationRequest.LifeTime.Transient => "AddTransient",
                _ => "AddTransient"
            };

            var usesTypeOfFormat = request.ConcreteType.StartsWith("typeof(");
            var hasInterface = request.InterfaceType != null;
            var useProvider = request.ResolveFromContainer;

            var concreteType = usesTypeOfFormat
                ? UseTypeOf(request.ConcreteType)
                : UseType(request.ConcreteType);

            var interfaceType = hasInterface
                ? usesTypeOfFormat
                    ? UseTypeOf(request.InterfaceType)
                    : UseType(request.InterfaceType)
                : null;

            // ReSharper disable ConditionIsAlwaysTrueOrFalse

            // This is a 3 way truth table to string mapping:
            return useProvider switch
            {
                false when !hasInterface && !usesTypeOfFormat => $"services.{registrationType}<{concreteType}>();",
                false when !hasInterface && usesTypeOfFormat => $"services.{registrationType}({concreteType});",
                false when hasInterface && !usesTypeOfFormat => $"services.{registrationType}<{interfaceType}, {concreteType}>();",
                false when hasInterface && usesTypeOfFormat => $"services.{registrationType}({interfaceType}, {concreteType});",
                true when !hasInterface && !usesTypeOfFormat => throw new InvalidOperationException($"Using a service provider for resolution during registration without an interface can cause an infinite loop. Concrete Type: {concreteType}"),
                true when !hasInterface && usesTypeOfFormat => throw new InvalidOperationException($"Using a service provider for resolution during registration without an interface can cause an infinite loop. Concrete Type: {concreteType}"),
                // These configurations can cause an infinite loop.
                // true when !hasInterface && !usesTypeOfFormat => $"services.{registrationType}(provider => provider.GetService<{concreteType}>());",
                // true when !hasInterface && usesTypeOfFormat => $"services.{registrationType}(provider => provider.GetService({concreteType}));",
                true when hasInterface && !usesTypeOfFormat => $"services.{registrationType}<{interfaceType}>(provider => provider.GetService<{concreteType}>());",
                true when hasInterface && usesTypeOfFormat => $"services.{registrationType}({interfaceType}, provider => provider.GetService({concreteType}));",
                _ => throw new InvalidOperationException()
            };
            // ReSharper restore ConditionIsAlwaysTrueOrFalse
        }

        private string ServiceConfigurationRegistration(ServiceConfigurationRequest registration)
        {
            string GetExtensionMethodParameterList()
            {
                if (registration.ExtensionMethodParameterList?.Any() != true)
                {
                    return string.Empty;
                }

                var paramList = new List<string>();

                foreach (var param in registration.ExtensionMethodParameterList)
                {
                    switch (param)
                    {
                        // Do we want to have Configuration as part of the Application registration?
                        // case ServiceConfigurationRequest.ParameterType.Configuration:
                        //     paramList.Add("configuration");
                        //     break;
                        default:
                            throw new ArgumentOutOfRangeException(
                                paramName: nameof(registration.ExtensionMethodParameterList),
                                actualValue: param,
                                message: "Type specified in parameter list is not known or supported");
                    }
                }

                return string.Join(", ", paramList);
            }

            return $"services.{registration.ExtensionMethodName}({GetExtensionMethodParameterList()});";
        }
    }
}