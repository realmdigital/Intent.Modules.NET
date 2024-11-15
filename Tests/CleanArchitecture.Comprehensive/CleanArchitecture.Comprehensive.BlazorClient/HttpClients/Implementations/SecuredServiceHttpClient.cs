using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using CleanArchitecture.Comprehensive.BlazorClient.HttpClients.Common;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: DefaultIntentManaged(Mode.Fully, Targets = Targets.Usings)]
[assembly: IntentTemplate("Intent.Blazor.HttpClients.HttpClient", Version = "2.0")]

namespace CleanArchitecture.Comprehensive.BlazorClient.HttpClients.Implementations
{
    public class SecuredServiceHttpClient : ISecuredService
    {
        public const string JSON_MEDIA_TYPE = "application/json";
        private readonly HttpClient _httpClient;

        public SecuredServiceHttpClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task SecuredAsync(CancellationToken cancellationToken = default)
        {
            var relativeUri = $"api/secured-service/secured";
            var httpRequest = new HttpRequestMessage(HttpMethod.Put, relativeUri);
            httpRequest.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(JSON_MEDIA_TYPE));

            using (var response = await _httpClient.SendAsync(httpRequest, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false))
            {
                if (!response.IsSuccessStatusCode)
                {
                    throw await HttpClientRequestException.Create(_httpClient.BaseAddress!, httpRequest, response, cancellationToken).ConfigureAwait(false);
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            // Class cleanup goes here
        }
    }
}