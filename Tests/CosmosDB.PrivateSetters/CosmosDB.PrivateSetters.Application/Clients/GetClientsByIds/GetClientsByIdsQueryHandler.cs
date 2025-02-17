using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CosmosDB.PrivateSetters.Domain.Repositories;
using Intent.RoslynWeaver.Attributes;
using MediatR;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.QueryHandler", Version = "1.0")]

namespace CosmosDB.PrivateSetters.Application.Clients.GetClientsByIds
{
    [IntentManaged(Mode.Merge, Signature = Mode.Fully)]
    public class GetClientsByIdsQueryHandler : IRequestHandler<GetClientsByIdsQuery, List<ClientDto>>
    {
        private readonly IClientRepository _clientRepository;
        private readonly IMapper _mapper;

        [IntentManaged(Mode.Merge)]
        public GetClientsByIdsQueryHandler(IClientRepository clientRepository, IMapper mapper)
        {
            _clientRepository = clientRepository;
            _mapper = mapper;
        }

        [IntentManaged(Mode.Fully, Body = Mode.Fully)]
        public async Task<List<ClientDto>> Handle(GetClientsByIdsQuery request, CancellationToken cancellationToken)
        {
            var clients = await _clientRepository.FindAllAsync(cancellationToken);
            return clients.MapToClientDtoList(_mapper);
        }
    }
}