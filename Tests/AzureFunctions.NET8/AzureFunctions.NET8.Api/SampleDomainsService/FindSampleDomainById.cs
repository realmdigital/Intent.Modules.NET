using System.Net;
using AzureFunctions.NET8.Application.Interfaces;
using AzureFunctions.NET8.Application.SampleDomains;
using AzureFunctions.NET8.Domain.Common.Exceptions;
using Intent.RoslynWeaver.Attributes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.OpenApi.Models;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.AzureFunctions.AzureFunctionClass", Version = "2.0")]

namespace AzureFunctions.NET8.Api.SampleDomainsService
{
    public class FindSampleDomainById
    {
        private readonly ISampleDomainsService _appService;

        public FindSampleDomainById(ISampleDomainsService appService)
        {
            _appService = appService ?? throw new ArgumentNullException(nameof(appService));
        }

        [Function("SampleDomainsService_FindSampleDomainById")]
        [OpenApiOperation("FindSampleDomainById", tags: new[] { "SampleDomains" }, Description = "Find sample domain by id")]
        [OpenApiParameter(name: "id", In = ParameterLocation.Path, Required = true, Type = typeof(Guid))]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(SampleDomainDto))]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(object))]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "sample-domains/{id}")] HttpRequest req,
            Guid id,
            CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await _appService.FindSampleDomainById(id, cancellationToken);
                return new OkObjectResult(result);
            }
            catch (NotFoundException exception)
            {
                return new NotFoundObjectResult(new { Message = exception.Message });
            }
            catch (FormatException exception)
            {
                return new BadRequestObjectResult(new { Message = exception.Message });
            }
        }
    }
}