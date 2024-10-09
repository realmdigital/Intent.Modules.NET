using System;
using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Comprehensive.Application.CustomResponseCodes.Post.CustomResponseDefaultWithResponse;
using FastEndpoints;
using Intent.RoslynWeaver.Attributes;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Mode = Intent.RoslynWeaver.Attributes.Mode;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.FastEndpoints.EndpointTemplate", Version = "1.0")]

namespace CleanArchitecture.Comprehensive.Api.FastEndpoints.CustomResponseCodes.Post
{
    public class CustomResponseDefaultWithResponseEndpoint : EndpointWithoutRequest<string>
    {
        private readonly ISender _mediator;

        public CustomResponseDefaultWithResponseEndpoint(ISender mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public override void Configure()
        {
            Post("api/custom-response-codes/custom-response-default-response");
            Description(b =>
            {
                b.WithTags("CustomResponseCodesPost");
                b.Produces<string>(StatusCodes.Status201Created);
                b.ProducesProblemDetails(StatusCodes.Status500InternalServerError);
            });
            AllowAnonymous();
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            var result = default(string);
            result = await _mediator.Send(new CustomResponseDefaultWithResponse(), ct);
            await SendResultAsync(TypedResults.Created(string.Empty, result));
        }
    }
}