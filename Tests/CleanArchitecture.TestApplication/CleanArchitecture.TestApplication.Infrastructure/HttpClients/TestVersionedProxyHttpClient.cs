using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.TestApplication.Application.Common.Exceptions;
using CleanArchitecture.TestApplication.Application.IntegrationServices;
using CleanArchitecture.TestApplication.Application.IntegrationServices.Services.Versioned;
using Intent.RoslynWeaver.Attributes;
using Microsoft.AspNetCore.WebUtilities;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: DefaultIntentManaged(Mode.Fully, Targets = Targets.Usings)]
[assembly: IntentTemplate("Intent.Integration.HttpClients.HttpClient", Version = "2.0")]

namespace CleanArchitecture.TestApplication.Infrastructure.HttpClients
{
    public class TestVersionedProxyHttpClient : ITestVersionedProxy
    {
        private readonly JsonSerializerOptions _serializerOptions;
        private readonly HttpClient _httpClient;

        public TestVersionedProxyHttpClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _serializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };
        }

        public async Task TestCommandV1Async(TestCommandV1 command, CancellationToken cancellationToken = default)
        {
            var relativeUri = $"api/v1/versioned/test-command";
            var request = new HttpRequestMessage(HttpMethod.Post, relativeUri);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var content = JsonSerializer.Serialize(command, _serializerOptions);
            request.Content = new StringContent(content, Encoding.Default, "application/json");

            using (var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false))
            {
                if (!response.IsSuccessStatusCode)
                {
                    throw await HttpClientRequestException.Create(_httpClient.BaseAddress!, request, response, cancellationToken).ConfigureAwait(false);
                }
            }
        }

        public async Task TestCommandV2Async(TestCommandV2 command, CancellationToken cancellationToken = default)
        {
            var relativeUri = $"api/v2/versioned/test-command";
            var request = new HttpRequestMessage(HttpMethod.Post, relativeUri);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var content = JsonSerializer.Serialize(command, _serializerOptions);
            request.Content = new StringContent(content, Encoding.Default, "application/json");

            using (var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false))
            {
                if (!response.IsSuccessStatusCode)
                {
                    throw await HttpClientRequestException.Create(_httpClient.BaseAddress!, request, response, cancellationToken).ConfigureAwait(false);
                }
            }
        }

        public async Task<int> TestQueryV1Async(string value, CancellationToken cancellationToken = default)
        {
            var relativeUri = $"api/v1/versioned/test-query";

            var queryParams = new Dictionary<string, string?>();
            queryParams.Add("value", value);
            relativeUri = QueryHelpers.AddQueryString(relativeUri, queryParams);
            var request = new HttpRequestMessage(HttpMethod.Get, relativeUri);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            using (var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false))
            {
                if (!response.IsSuccessStatusCode)
                {
                    throw await HttpClientRequestException.Create(_httpClient.BaseAddress!, request, response, cancellationToken).ConfigureAwait(false);
                }

                using (var contentStream = await response.Content.ReadAsStreamAsync(cancellationToken).ConfigureAwait(false))
                {
                    var str = await new StreamReader(contentStream).ReadToEndAsync(cancellationToken).ConfigureAwait(false);
                    if (str != null && (str.StartsWith(@"""") || str.StartsWith("'"))) { str = str.Substring(1, str.Length - 2); };
                    return int.Parse(str);
                }
            }
        }

        public async Task<int> TestQueryV2Async(string value, CancellationToken cancellationToken = default)
        {
            var relativeUri = $"api/v2/versioned/test-query";

            var queryParams = new Dictionary<string, string?>();
            queryParams.Add("value", value);
            relativeUri = QueryHelpers.AddQueryString(relativeUri, queryParams);
            var request = new HttpRequestMessage(HttpMethod.Get, relativeUri);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            using (var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false))
            {
                if (!response.IsSuccessStatusCode)
                {
                    throw await HttpClientRequestException.Create(_httpClient.BaseAddress!, request, response, cancellationToken).ConfigureAwait(false);
                }

                using (var contentStream = await response.Content.ReadAsStreamAsync(cancellationToken).ConfigureAwait(false))
                {
                    var str = await new StreamReader(contentStream).ReadToEndAsync(cancellationToken).ConfigureAwait(false);
                    if (str != null && (str.StartsWith(@"""") || str.StartsWith("'"))) { str = str.Substring(1, str.Length - 2); };
                    return int.Parse(str);
                }
            }
        }

        public void Dispose()
        {
        }
    }
}