﻿// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 16.0.0.0
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------
namespace Intent.Modules.Integration.HttpClients.Templates.HttpClient
{
    using System.Linq;
    using Intent.Modules.Common;
    using Intent.Modules.Common.Templates;
    using Intent.Modules.Common.CSharp.Templates;
    using Intent.Modules.Application.Contracts.Clients.Templates;
    using Intent.Modules.Application.Contracts.Clients;
    using System;
    
    /// <summary>
    /// Class to produce the template output
    /// </summary>
    
    #line 1 "C:\Dev\Intent.Modules.NET\Modules\Intent.Modules.Integration.HttpClients\Templates\HttpClient\HttpClientTemplate.tt"
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "16.0.0.0")]
    public partial class HttpClientTemplate : CSharpTemplateBase<Intent.Modelers.Types.ServiceProxies.Api.ServiceProxyModel>
    {
#line hidden
        /// <summary>
        /// Create the template output
        /// </summary>
        public override string TransformText()
        {
            this.Write("using System;\r\nusing System.Collections.Generic;\r\nusing System.IO;\r\nusing System.Linq;\r\nusing System.Net;\r\nusing System.Net.Http;\r\nusing System.Net.Http.Headers;\r\nusing System.Text;\r\nusing System.Text.Json;\r\nusing System.Threading;\r\nusing System.Threading.Tasks;\r\nusing Microsoft.AspNetCore.WebUtilities;\r\n\r\n[assembly: DefaultIntentManaged(Mode.Fully)]\r\n\r\nnamespace ");
            
            #line 24 "C:\Dev\Intent.Modules.NET\Modules\Intent.Modules.Integration.HttpClients\Templates\HttpClient\HttpClientTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Namespace));
            
            #line default
            #line hidden
            this.Write("\r\n{\r\n    public class ");
            
            #line 26 "C:\Dev\Intent.Modules.NET\Modules\Intent.Modules.Integration.HttpClients\Templates\HttpClient\HttpClientTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(ClassName));
            
            #line default
            #line hidden
            this.Write(" : ");
            
            #line 26 "C:\Dev\Intent.Modules.NET\Modules\Intent.Modules.Integration.HttpClients\Templates\HttpClient\HttpClientTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.GetServiceContractName()));
            
            #line default
            #line hidden
            this.Write("\r\n    {\r\n        private readonly HttpClient _httpClient;\r\n\r\n        private readonly JsonSerializerOptions _serializerOptions = new JsonSerializerOptions()\r\n        {\r\n            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,\r\n        };\r\n\r\n        public ");
            
            #line 35 "C:\Dev\Intent.Modules.NET\Modules\Intent.Modules.Integration.HttpClients\Templates\HttpClient\HttpClientTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(ClassName));
            
            #line default
            #line hidden
            this.Write("(HttpClient httpClient)\r\n        {\r\n            _httpClient = httpClient;\r\n        }\r\n\r\n");
            
            #line 40 "C:\Dev\Intent.Modules.NET\Modules\Intent.Modules.Integration.HttpClients\Templates\HttpClient\HttpClientTemplate.tt"

    foreach (var operation in Model.MappedService.Operations.Where(ContractMetadataQueries .IsAbleToReference))
    {

            
            #line default
            #line hidden
            this.Write("        public async ");
            
            #line 44 "C:\Dev\Intent.Modules.NET\Modules\Intent.Modules.Integration.HttpClients\Templates\HttpClient\HttpClientTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(GetReturnType(operation)));
            
            #line default
            #line hidden
            this.Write(" ");
            
            #line 44 "C:\Dev\Intent.Modules.NET\Modules\Intent.Modules.Integration.HttpClients\Templates\HttpClient\HttpClientTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(GetOperationName(operation)));
            
            #line default
            #line hidden
            this.Write("(");
            
            #line 44 "C:\Dev\Intent.Modules.NET\Modules\Intent.Modules.Integration.HttpClients\Templates\HttpClient\HttpClientTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(GetOperationParameters(operation)));
            
            #line default
            #line hidden
            this.Write(")\r\n        {");
            
            #line 45 "C:\Dev\Intent.Modules.NET\Modules\Intent.Modules.Integration.HttpClients\Templates\HttpClient\HttpClientTemplate.tt"

        // We're leveraging the C# $"" notation to actually take leverage of the parameters
        // that are meant to be Route-based.

            
            #line default
            #line hidden
            this.Write("            var relativeUri = $\"");
            
            #line 49 "C:\Dev\Intent.Modules.NET\Modules\Intent.Modules.Integration.HttpClients\Templates\HttpClient\HttpClientTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(GetRelativeUri(operation)));
            
            #line default
            #line hidden
            this.Write("\";\r\n");
            
            #line 50 "C:\Dev\Intent.Modules.NET\Modules\Intent.Modules.Integration.HttpClients\Templates\HttpClient\HttpClientTemplate.tt"

        if (HasQueryParameter(operation))
        {

            
            #line default
            #line hidden
            this.Write("            \r\n            var queryParams = new Dictionary<string, string>();\r\n");
            
            #line 56 "C:\Dev\Intent.Modules.NET\Modules\Intent.Modules.Integration.HttpClients\Templates\HttpClient\HttpClientTemplate.tt"

            foreach (var queryParameter in GetQueryParameters(operation))
            {

            
            #line default
            #line hidden
            this.Write("            queryParams.Add(\"");
            
            #line 60 "C:\Dev\Intent.Modules.NET\Modules\Intent.Modules.Integration.HttpClients\Templates\HttpClient\HttpClientTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(queryParameter.Name.ToCamelCase()));
            
            #line default
            #line hidden
            this.Write("\", ");
            
            #line 60 "C:\Dev\Intent.Modules.NET\Modules\Intent.Modules.Integration.HttpClients\Templates\HttpClient\HttpClientTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(GetParameterValueExpression(queryParameter)));
            
            #line default
            #line hidden
            this.Write(");\r\n");
            
            #line 61 "C:\Dev\Intent.Modules.NET\Modules\Intent.Modules.Integration.HttpClients\Templates\HttpClient\HttpClientTemplate.tt"

            }

            
            #line default
            #line hidden
            this.Write("            relativeUri = QueryHelpers.AddQueryString(relativeUri, queryParams);\r\n\r\n");
            
            #line 66 "C:\Dev\Intent.Modules.NET\Modules\Intent.Modules.Integration.HttpClients\Templates\HttpClient\HttpClientTemplate.tt"

        }

            
            #line default
            #line hidden
            this.Write("            var request = new HttpRequestMessage(");
            
            #line 69 "C:\Dev\Intent.Modules.NET\Modules\Intent.Modules.Integration.HttpClients\Templates\HttpClient\HttpClientTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(GetHttpVerb(operation)));
            
            #line default
            #line hidden
            this.Write(", relativeUri);\r\n            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(\"application/json\"));\r\n");
            
            #line 71 "C:\Dev\Intent.Modules.NET\Modules\Intent.Modules.Integration.HttpClients\Templates\HttpClient\HttpClientTemplate.tt"

        foreach (var headerParameter in GetHeaderParameters(operation))
        {

            
            #line default
            #line hidden
            this.Write("            request.Headers.Add(\"");
            
            #line 75 "C:\Dev\Intent.Modules.NET\Modules\Intent.Modules.Integration.HttpClients\Templates\HttpClient\HttpClientTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(headerParameter.HeaderName));
            
            #line default
            #line hidden
            this.Write("\", ");
            
            #line 75 "C:\Dev\Intent.Modules.NET\Modules\Intent.Modules.Integration.HttpClients\Templates\HttpClient\HttpClientTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(headerParameter.Parameter.Name.ToParameterName()));
            
            #line default
            #line hidden
            this.Write(");\r\n\r\n");
            
            #line 77 "C:\Dev\Intent.Modules.NET\Modules\Intent.Modules.Integration.HttpClients\Templates\HttpClient\HttpClientTemplate.tt"

        }

        if (HasBodyParameter(operation))
        {

            
            #line default
            #line hidden
            this.Write("            \r\n            var content = JsonSerializer.Serialize(");
            
            #line 84 "C:\Dev\Intent.Modules.NET\Modules\Intent.Modules.Integration.HttpClients\Templates\HttpClient\HttpClientTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(GetBodyParameterName(operation)));
            
            #line default
            #line hidden
            this.Write(", _serializerOptions);\r\n            request.Content = new StringContent(content, Encoding.Default, \"application/json\");\r\n\r\n");
            
            #line 87 "C:\Dev\Intent.Modules.NET\Modules\Intent.Modules.Integration.HttpClients\Templates\HttpClient\HttpClientTemplate.tt"

        }
        else if (HasFormUrlEncodedParameter(operation))
        {

            
            #line default
            #line hidden
            this.Write("            \r\n            var formVariables = new List<KeyValuePair<string, string>>();\r\n");
            
            #line 94 "C:\Dev\Intent.Modules.NET\Modules\Intent.Modules.Integration.HttpClients\Templates\HttpClient\HttpClientTemplate.tt"

            foreach (var formParameter in GetFormUrlEncodedParameters(operation))
            {

            
            #line default
            #line hidden
            this.Write("            formVariables.Add(new KeyValuePair<string, string>(\"");
            
            #line 98 "C:\Dev\Intent.Modules.NET\Modules\Intent.Modules.Integration.HttpClients\Templates\HttpClient\HttpClientTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(formParameter.Name.ToPascalCase()));
            
            #line default
            #line hidden
            this.Write("\", ");
            
            #line 98 "C:\Dev\Intent.Modules.NET\Modules\Intent.Modules.Integration.HttpClients\Templates\HttpClient\HttpClientTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(GetParameterValueExpression(formParameter)));
            
            #line default
            #line hidden
            this.Write("));\r\n");
            
            #line 99 "C:\Dev\Intent.Modules.NET\Modules\Intent.Modules.Integration.HttpClients\Templates\HttpClient\HttpClientTemplate.tt"

            }

            
            #line default
            #line hidden
            this.Write("            var content = new FormUrlEncodedContent(formVariables);\r\n            request.Content = content;\r\n");
            
            #line 104 "C:\Dev\Intent.Modules.NET\Modules\Intent.Modules.Integration.HttpClients\Templates\HttpClient\HttpClientTemplate.tt"

        }

            
            #line default
            #line hidden
            this.Write("            \r\n            using (var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false))\r\n            {\r\n                if (!response.IsSuccessStatusCode)\r\n                {\r\n                    throw await ");
            
            #line 112 "C:\Dev\Intent.Modules.NET\Modules\Intent.Modules.Integration.HttpClients\Templates\HttpClient\HttpClientTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.GetHttpClientRequestExceptionName()));
            
            #line default
            #line hidden
            this.Write(".Create(_httpClient.BaseAddress, request, response, cancellationToken).ConfigureAwait(false);\r\n                }\r\n");
            
            #line 114 "C:\Dev\Intent.Modules.NET\Modules\Intent.Modules.Integration.HttpClients\Templates\HttpClient\HttpClientTemplate.tt"

        if (HasResponseType(operation))
        {

            
            #line default
            #line hidden
            this.Write("                if (response.StatusCode == HttpStatusCode.NoContent || response.Content.Headers.ContentLength == 0)\r\n                {\r\n                    return default;\r\n                }\r\n\r\n                using (var contentStream = await response.Content.ReadAsStreamAsync(cancellationToken).ConfigureAwait(false))\r\n                {\r\n");
            
            #line 125 "C:\Dev\Intent.Modules.NET\Modules\Intent.Modules.Integration.HttpClients\Templates\HttpClient\HttpClientTemplate.tt"

            if (HasWrappedReturnType(operation))
            {

            
            #line default
            #line hidden
            this.Write("                    var wrappedObj = await JsonSerializer.DeserializeAsync<");
            
            #line 129 "C:\Dev\Intent.Modules.NET\Modules\Intent.Modules.Integration.HttpClients\Templates\HttpClient\HttpClientTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.GetJsonResponseName()));
            
            #line default
            #line hidden
            this.Write("<");
            
            #line 129 "C:\Dev\Intent.Modules.NET\Modules\Intent.Modules.Integration.HttpClients\Templates\HttpClient\HttpClientTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(GetTypeName(operation.ReturnType)));
            
            #line default
            #line hidden
            this.Write(">>(contentStream, _serializerOptions, cancellationToken).ConfigureAwait(false);\r\n                    return wrappedObj.Value;\r\n");
            
            #line 131 "C:\Dev\Intent.Modules.NET\Modules\Intent.Modules.Integration.HttpClients\Templates\HttpClient\HttpClientTemplate.tt"
   
            }
            else if (operation.ReturnType.HasStringType() && !operation.ReturnType.IsCollection)
            {

            
            #line default
            #line hidden
            this.Write("                    var str = await new StreamReader(contentStream).ReadToEndAsync().ConfigureAwait(false);\r\n                    if (str.StartsWith(@\"\"\"\") || str.StartsWith(\"'\")) { str = str.Substring(1, str.Length - 2); }\r\n                    return str;\r\n");
            
            #line 139 "C:\Dev\Intent.Modules.NET\Modules\Intent.Modules.Integration.HttpClients\Templates\HttpClient\HttpClientTemplate.tt"

            }
            else if (IsReturnTypePrimitive(operation))
            {

            
            #line default
            #line hidden
            this.Write("                    var str = await new StreamReader(contentStream).ReadToEndAsync().ConfigureAwait(false);\r\n                    if (str.StartsWith(@\"\"\"\") || str.StartsWith(\"'\")) { str = str.Substring(1, str.Length - 2); }\r\n                    return ");
            
            #line 146 "C:\Dev\Intent.Modules.NET\Modules\Intent.Modules.Integration.HttpClients\Templates\HttpClient\HttpClientTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(GetTypeName(operation.ReturnType)));
            
            #line default
            #line hidden
            this.Write(".Parse(str);\r\n");
            
            #line 147 "C:\Dev\Intent.Modules.NET\Modules\Intent.Modules.Integration.HttpClients\Templates\HttpClient\HttpClientTemplate.tt"
              
            }
            else
            {

            
            #line default
            #line hidden
            this.Write("                    return await JsonSerializer.DeserializeAsync<");
            
            #line 152 "C:\Dev\Intent.Modules.NET\Modules\Intent.Modules.Integration.HttpClients\Templates\HttpClient\HttpClientTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(GetTypeName(operation.ReturnType)));
            
            #line default
            #line hidden
            this.Write(">(contentStream, _serializerOptions, cancellationToken).ConfigureAwait(false);\r\n");
            
            #line 153 "C:\Dev\Intent.Modules.NET\Modules\Intent.Modules.Integration.HttpClients\Templates\HttpClient\HttpClientTemplate.tt"

            }

            
            #line default
            #line hidden
            this.Write("                }\r\n");
            
            #line 157 "C:\Dev\Intent.Modules.NET\Modules\Intent.Modules.Integration.HttpClients\Templates\HttpClient\HttpClientTemplate.tt"

        }

            
            #line default
            #line hidden
            this.Write("            }\r\n        }\r\n");
            
            #line 162 "C:\Dev\Intent.Modules.NET\Modules\Intent.Modules.Integration.HttpClients\Templates\HttpClient\HttpClientTemplate.tt"

    }

            
            #line default
            #line hidden
            this.Write("\r\n        public void Dispose()\r\n        {\r\n        }\r\n    }\r\n}");
            return this.GenerationEnvironment.ToString();
        }
    }
    
}
