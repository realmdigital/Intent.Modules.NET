using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.TestApplication.Domain.DDD;
using CleanArchitecture.TestApplication.Domain.Entities.DDD;
using CleanArchitecture.TestApplication.Domain.Repositories.DDD;
using Intent.RoslynWeaver.Attributes;
using MediatR;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.CommandHandler", Version = "2.0")]

namespace CleanArchitecture.TestApplication.Application.DDD.CreateCustomer
{
    [IntentManaged(Mode.Merge, Signature = Mode.Fully)]
    public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, Guid>
    {
        private readonly ICustomerRepository _customerRepository;

        [IntentManaged(Mode.Merge)]
        public CreateCustomerCommandHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        [IntentManaged(Mode.Fully, Body = Mode.Fully)]
        public async Task<Guid> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            var newCustomer = new Customer
            {
                Name = request.Name,
                Surname = request.Surname,
                Email = request.Email,
                Address = CreateAddress(request.Address),
            };

            _customerRepository.Add(newCustomer);
            await _customerRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
            return newCustomer.Id;
        }

        [IntentManaged(Mode.Fully)]
        public static Address CreateAddress(CreateCustomerAddressDto dto)
        {
            return new Address(line1: dto.Line1, line2: dto.Line2, city: dto.City, postal: dto.Postal);
        }
    }
}