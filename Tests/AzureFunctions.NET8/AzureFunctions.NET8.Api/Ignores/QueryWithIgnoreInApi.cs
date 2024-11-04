using System.Net;
using AzureFunctions.NET8.Application.Ignores.QueryWithIgnoreInApi;
using AzureFunctions.NET8.Domain.Common.Exceptions;
using Intent.RoslynWeaver.Attributes;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.OpenApi.Models;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.AzureFunctions.AzureFunctionClass", Version = "2.0")]

namespace AzureFunctions.NET8.Api.Ignores
{
    public class QueryWithIgnoreInApi
    {
        private readonly IMediator _mediator;

        public QueryWithIgnoreInApi(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [Function("Ignores_QueryWithIgnoreInApi")]
        [OpenApiOperation("QueryWithIgnoreInApi", tags: new[] { "Ignores" }, Description = "Query with ignore in api")]
        [OpenApiIgnoreAttribute]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(bool))]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(object))]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "ignores/query-with-ignore-in-api")] HttpRequest req,
            CancellationToken cancellationToken)
        {
            try
            {
                var result = await _mediator.Send(new Application.Ignores.QueryWithIgnoreInApi.QueryWithIgnoreInApi(), cancellationToken);
                return result != null ? new OkObjectResult(result) : new NotFoundResult();
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