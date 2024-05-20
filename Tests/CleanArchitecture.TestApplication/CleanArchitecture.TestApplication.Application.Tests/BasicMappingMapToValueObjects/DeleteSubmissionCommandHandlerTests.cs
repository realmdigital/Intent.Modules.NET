using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using CleanArchitecture.TestApplication.Application.BasicMappingMapToValueObjects.DeleteSubmission;
using CleanArchitecture.TestApplication.Domain.Common;
using CleanArchitecture.TestApplication.Domain.Common.Exceptions;
using CleanArchitecture.TestApplication.Domain.Entities.BasicMappingMapToValueObjects;
using CleanArchitecture.TestApplication.Domain.Repositories.BasicMappingMapToValueObjects;
using FluentAssertions;
using Intent.RoslynWeaver.Attributes;
using NSubstitute;
using Xunit;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.CRUD.Tests.Owner.DeleteCommandHandlerTests", Version = "1.0")]

namespace CleanArchitecture.TestApplication.Application.Tests.BasicMappingMapToValueObjects
{
    public class DeleteSubmissionCommandHandlerTests
    {
        public static IEnumerable<object[]> GetSuccessfulResultTestData()
        {
            var fixture = new Fixture();
            fixture.Register<DomainEvent>(() => null!);
            var existingEntity = fixture.Create<Submission>();
            fixture.Customize<DeleteSubmissionCommand>(comp => comp.With(x => x.Id, existingEntity.Id));
            var testCommand = fixture.Create<DeleteSubmissionCommand>();
            yield return new object[] { testCommand, existingEntity };
        }

        [Theory]
        [MemberData(nameof(GetSuccessfulResultTestData))]
        public async Task Handle_WithValidCommand_DeletesSubmissionFromRepository(
            DeleteSubmissionCommand testCommand,
            Submission existingEntity)
        {
            // Arrange
            var submissionRepository = Substitute.For<ISubmissionRepository>();
            submissionRepository.FindByIdAsync(testCommand.Id, CancellationToken.None)!.Returns(Task.FromResult(existingEntity));

            var sut = new DeleteSubmissionCommandHandler(submissionRepository);

            // Act
            await sut.Handle(testCommand, CancellationToken.None);

            // Assert
            submissionRepository.Received(1).Remove(Arg.Is<Submission>(p => testCommand.Id == p.Id));
        }

        [Fact]
        public async Task Handle_WithInvalidSubmissionId_ReturnsNotFound()
        {
            // Arrange
            var submissionRepository = Substitute.For<ISubmissionRepository>();
            var fixture = new Fixture();
            var testCommand = fixture.Create<DeleteSubmissionCommand>();
            submissionRepository.FindByIdAsync(testCommand.Id, CancellationToken.None)!.Returns(Task.FromResult<Submission>(default));


            var sut = new DeleteSubmissionCommandHandler(submissionRepository);

            // Act
            var act = async () => await sut.Handle(testCommand, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<NotFoundException>();
        }
    }
}