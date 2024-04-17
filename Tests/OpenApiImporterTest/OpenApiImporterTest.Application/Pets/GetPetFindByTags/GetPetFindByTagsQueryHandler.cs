using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Intent.RoslynWeaver.Attributes;
using MediatR;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.QueryHandler", Version = "1.0")]

namespace OpenApiImporterTest.Application.Pets.GetPetFindByTags
{
    [IntentManaged(Mode.Merge, Signature = Mode.Fully)]
    public class GetPetFindByTagsQueryHandler : IRequestHandler<GetPetFindByTagsQuery, List<Pet>>
    {
        [IntentManaged(Mode.Merge)]
        public GetPetFindByTagsQueryHandler()
        {
        }

        [IntentManaged(Mode.Fully, Body = Mode.Ignore)]
        public async Task<List<Pet>> Handle(GetPetFindByTagsQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException("Your implementation here...");
        }
    }
}