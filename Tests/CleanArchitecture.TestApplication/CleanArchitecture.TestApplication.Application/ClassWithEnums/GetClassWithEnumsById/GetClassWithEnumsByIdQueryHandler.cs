using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CleanArchitecture.TestApplication.Domain.Repositories.Enums;
using Intent.RoslynWeaver.Attributes;
using MediatR;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.QueryHandler", Version = "1.0")]

namespace CleanArchitecture.TestApplication.Application.ClassWithEnums.GetClassWithEnumsById
{
    [IntentManaged(Mode.Merge, Signature = Mode.Fully)]
    public class GetClassWithEnumsByIdQueryHandler : IRequestHandler<GetClassWithEnumsByIdQuery, ClassWithEnumsDto>
    {
        private readonly IClassWithEnumsRepository _classWithEnumsRepository;
        private readonly IMapper _mapper;

        [IntentManaged(Mode.Ignore)]
        public GetClassWithEnumsByIdQueryHandler(IClassWithEnumsRepository classWithEnumsRepository, IMapper mapper)
        {
            _classWithEnumsRepository = classWithEnumsRepository;
            _mapper = mapper;
        }

        [IntentManaged(Mode.Fully, Body = Mode.Fully)]
        public async Task<ClassWithEnumsDto> Handle(
            GetClassWithEnumsByIdQuery request,
            CancellationToken cancellationToken)
        {
            var classWithEnums = await _classWithEnumsRepository.FindByIdAsync(request.Id, cancellationToken);
            return classWithEnums.MapToClassWithEnumsDto(_mapper);
        }
    }
}