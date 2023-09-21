using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using CleanArchitecture.TestApplication.Application.Tests.Extensions;
using CleanArchitecture.TestApplication.Application.UniqueIndexConstraint.AggregateWithUniqueConstraintIndexStereotypes.CreateAggregateWithUniqueConstraintIndexStereotype;
using CleanArchitecture.TestApplication.Domain.Entities.UniqueIndexConstraint;
using CleanArchitecture.TestApplication.Domain.Repositories.UniqueIndexConstraint;
using FluentAssertions;
using Intent.RoslynWeaver.Attributes;
using NSubstitute;
using Xunit;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.CRUD.Tests.Owner.CreateCommandHandlerTests", Version = "1.0")]

namespace CleanArchitecture.TestApplication.Application.Tests.UniqueIndexConstraint.AggregateWithUniqueConstraintIndexStereotypes
{
    public class CreateAggregateWithUniqueConstraintIndexStereotypeCommandHandlerTests
    {
        public static IEnumerable<object[]> GetSuccessfulResultTestData()
        {
            var fixture = new Fixture();
            yield return new object[] { fixture.Create<CreateAggregateWithUniqueConstraintIndexStereotypeCommand>() };
        }

        [Theory]
        [MemberData(nameof(GetSuccessfulResultTestData))]
        public async Task Handle_WithValidCommand_AddsAggregateWithUniqueConstraintIndexStereotypeToRepository(CreateAggregateWithUniqueConstraintIndexStereotypeCommand testCommand)
        {
            // Arrange
            var aggregateWithUniqueConstraintIndexStereotypeRepository = Substitute.For<IAggregateWithUniqueConstraintIndexStereotypeRepository>();
            var expectedAggregateWithUniqueConstraintIndexStereotypeId = new Fixture().Create<System.Guid>();
            AggregateWithUniqueConstraintIndexStereotype addedAggregateWithUniqueConstraintIndexStereotype = null;
            aggregateWithUniqueConstraintIndexStereotypeRepository.OnAdd(ent => addedAggregateWithUniqueConstraintIndexStereotype = ent);
            aggregateWithUniqueConstraintIndexStereotypeRepository.UnitOfWork
                .When(async x => await x.SaveChangesAsync(CancellationToken.None))
                .Do(_ => addedAggregateWithUniqueConstraintIndexStereotype.Id = expectedAggregateWithUniqueConstraintIndexStereotypeId);

            var sut = new CreateAggregateWithUniqueConstraintIndexStereotypeCommandHandler(aggregateWithUniqueConstraintIndexStereotypeRepository);

            // Act
            var result = await sut.Handle(testCommand, CancellationToken.None);

            // Assert
            result.Should().Be(expectedAggregateWithUniqueConstraintIndexStereotypeId);
            await aggregateWithUniqueConstraintIndexStereotypeRepository.UnitOfWork.Received(1).SaveChangesAsync();
            AggregateWithUniqueConstraintIndexStereotypeAssertions.AssertEquivalent(testCommand, addedAggregateWithUniqueConstraintIndexStereotype);
        }
    }
}