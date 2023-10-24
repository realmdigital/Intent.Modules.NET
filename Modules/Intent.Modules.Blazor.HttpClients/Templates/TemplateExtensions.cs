using System.Collections.Generic;
using Intent.Modules.Blazor.HttpClients.Templates.DtoContract;
using Intent.Modules.Blazor.HttpClients.Templates.EnumContract;
using Intent.Modules.Blazor.HttpClients.Templates.HttpClient;
using Intent.Modules.Blazor.HttpClients.Templates.HttpClientConfiguration;
using Intent.Modules.Blazor.HttpClients.Templates.HttpClientRequestException;
using Intent.Modules.Blazor.HttpClients.Templates.JsonResponse;
using Intent.Modules.Blazor.HttpClients.Templates.PagedResult;
using Intent.Modules.Blazor.HttpClients.Templates.ServiceContract;
using Intent.Modules.Common.Templates;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: DefaultIntentManaged(Mode.Fully, Targets = Targets.Usings)]
[assembly: IntentTemplate("Intent.ModuleBuilder.Templates.TemplateExtensions", Version = "1.0")]

namespace Intent.Modules.Blazor.HttpClients.Templates
{
    public static class TemplateExtensions
    {
        public static string GetDtoContractName<T>(this IIntentTemplate<T> template) where T : Intent.Modelers.Services.Api.DTOModel
        {
            return template.GetTypeName(DtoContractTemplate.TemplateId, template.Model);
        }

        public static string GetDtoContractName(this IIntentTemplate template, Intent.Modelers.Services.Api.DTOModel model)
        {
            return template.GetTypeName(DtoContractTemplate.TemplateId, model);
        }

        public static string GetEnumContractName<T>(this IIntentTemplate<T> template) where T : Intent.Modules.Common.Types.Api.EnumModel
        {
            return template.GetTypeName(EnumContractTemplate.TemplateId, template.Model);
        }

        public static string GetEnumContractName(this IIntentTemplate template, Intent.Modules.Common.Types.Api.EnumModel model)
        {
            return template.GetTypeName(EnumContractTemplate.TemplateId, model);
        }
        public static string GetHttpClientName<T>(this IIntentTemplate<T> template) where T : Intent.Modelers.Types.ServiceProxies.Api.ServiceProxyModel
        {
            return template.GetTypeName(HttpClientTemplate.TemplateId, template.Model);
        }

        public static string GetHttpClientName(this IIntentTemplate template, Intent.Modelers.Types.ServiceProxies.Api.ServiceProxyModel model)
        {
            return template.GetTypeName(HttpClientTemplate.TemplateId, model);
        }

        public static string GetHttpClientConfigurationName(this IIntentTemplate template)
        {
            return template.GetTypeName(HttpClientConfigurationTemplate.TemplateId);
        }

        public static string GetHttpClientRequestExceptionName(this IIntentTemplate template)
        {
            return template.GetTypeName(HttpClientRequestExceptionTemplate.TemplateId);
        }

        public static string GetJsonResponseName(this IIntentTemplate template)
        {
            return template.GetTypeName(JsonResponseTemplate.TemplateId);
        }

        public static string GetPagedResultName(this IIntentTemplate template)
        {
            return template.GetTypeName(PagedResultTemplate.TemplateId);
        }

        public static string GetServiceContractName<T>(this IIntentTemplate<T> template) where T : Intent.Modelers.Types.ServiceProxies.Api.ServiceProxyModel
        {
            return template.GetTypeName(ServiceContractTemplate.TemplateId, template.Model);
        }

        public static string GetServiceContractName(this IIntentTemplate template, Intent.Modelers.Types.ServiceProxies.Api.ServiceProxyModel model)
        {
            return template.GetTypeName(ServiceContractTemplate.TemplateId, model);
        }

    }
}