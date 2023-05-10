using System;
using System.Threading;
using System.Threading.Tasks;
using GraphQL.CQRS.TestApplication.Application.Customers;
using GraphQL.CQRS.TestApplication.Application.Customers.CreateCustomer;
using GraphQL.CQRS.TestApplication.Application.Customers.DeleteCustomer;
using GraphQL.CQRS.TestApplication.Application.Customers.UpdateCustomer;
using HotChocolate;
using HotChocolate.Types;
using Intent.RoslynWeaver.Attributes;
using MediatR;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.HotChocolate.GraphQL.MutationResolver", Version = "1.0")]

namespace GraphQL.CQRS.TestApplication.Api.GraphQL.MutationResolvers
{
    [ExtendObjectType(Name = "Mutation")]
    public class CustomerMutations
    {
        public async Task<Guid> CreateCustomer(
            CreateCustomerCommand input,
            CancellationToken cancellationToken,
            [Service] ISender mediator)
        {
            return await mediator.Send(input, cancellationToken);
        }

        public async Task<CustomerDto> DeleteCustomer(
            DeleteCustomerCommand input,
            CancellationToken cancellationToken,
            [Service] ISender mediator)
        {
            return await mediator.Send(input, cancellationToken);
        }

        public async Task<CustomerDto> UpdateCustomer(
            UpdateCustomerCommand input,
            CancellationToken cancellationToken,
            [Service] ISender mediator)
        {
            return await mediator.Send(input, cancellationToken);
        }
    }
}