using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using AzureFunctions.TestApplication.Application.Interfaces;
using AzureFunctions.TestApplication.Domain.Common.Exceptions;
using FluentValidation;
using Intent.RoslynWeaver.Attributes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.AzureFunctions.AzureFunctionClass", Version = "1.0")]

namespace AzureFunctions.TestApplication.Api
{
    public class ListedServiceFunc
    {
        private readonly IListedUnlistedServicesService _appService;

        public ListedServiceFunc(IListedUnlistedServicesService appService)
        {
            _appService = appService ?? throw new ArgumentNullException(nameof(appService));
        }

        [FunctionName("ListedServiceFunc")]
        [OpenApiOperation("ListedServiceFunc", tags: new[] { "ListedUnlistedServices" }, Description = "Listed service func")]
        [OpenApiParameter(name: "param", In = ParameterLocation.Query, Required = true, Type = typeof(string))]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(object))]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "listed-unlisted-services/listed-service-func")] HttpRequest req,
            CancellationToken cancellationToken)
        {
            try
            {
                string param = req.Query["param"];
                await _appService.ListedServiceFunc(param);
                return new CreatedResult(string.Empty, null);
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