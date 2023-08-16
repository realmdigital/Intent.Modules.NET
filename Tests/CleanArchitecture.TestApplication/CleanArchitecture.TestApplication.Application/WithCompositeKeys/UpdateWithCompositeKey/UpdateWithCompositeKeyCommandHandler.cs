using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.TestApplication.Domain.Common.Exceptions;
using CleanArchitecture.TestApplication.Domain.Repositories;
using CleanArchitecture.TestApplication.Domain.Repositories.CompositeKeys;
using Intent.RoslynWeaver.Attributes;
using MediatR;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.CommandHandler", Version = "2.0")]

namespace CleanArchitecture.TestApplication.Application.WithCompositeKeys.UpdateWithCompositeKey
{
    [IntentManaged(Mode.Merge, Signature = Mode.Fully)]
    public class UpdateWithCompositeKeyCommandHandler : IRequestHandler<UpdateWithCompositeKeyCommand>
    {
        private readonly IWithCompositeKeyRepository _withCompositeKeyRepository;

        [IntentManaged(Mode.Merge)]
        public UpdateWithCompositeKeyCommandHandler(IWithCompositeKeyRepository withCompositeKeyRepository)
        {
            _withCompositeKeyRepository = withCompositeKeyRepository;
        }

        [IntentManaged(Mode.Fully, Body = Mode.Fully)]
        public async Task Handle(UpdateWithCompositeKeyCommand request, CancellationToken cancellationToken)
        {
            var existingWithCompositeKey = await _withCompositeKeyRepository.FindByIdAsync((request.Key1Id, request.Key2Id), cancellationToken);
            if (existingWithCompositeKey is null)
            {
                throw new NotFoundException($"Could not find WithCompositeKey '({request.Key1Id}, {request.Key2Id})'");
            }

            existingWithCompositeKey.Name = request.Name;
            existingWithCompositeKey.Key1Id = request.Key1Id;
            existingWithCompositeKey.Key2Id = request.Key2Id;
        }
    }
}