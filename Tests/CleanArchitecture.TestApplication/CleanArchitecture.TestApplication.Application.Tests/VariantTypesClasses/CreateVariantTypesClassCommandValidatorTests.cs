using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using CleanArchitecture.TestApplication.Application.Common.Behaviours;
using CleanArchitecture.TestApplication.Application.VariantTypesClasses.CreateVariantTypesClass;
using FluentAssertions;
using FluentValidation;
using Intent.RoslynWeaver.Attributes;
using MediatR;
using NSubstitute;
using Xunit;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.CRUD.Tests.FluentValidation.FluentValidationTest", Version = "1.0")]

namespace CleanArchitecture.TestApplication.Application.Tests.VariantTypesClasses
{
    public class CreateVariantTypesClassCommandValidatorTests
    {
        public static IEnumerable<object[]> GetSuccessfulResultTestData()
        {
            var fixture = new Fixture();
            var testCommand = fixture.Create<CreateVariantTypesClassCommand>();
            yield return new object[] { testCommand };
        }

        [Theory]
        [MemberData(nameof(GetSuccessfulResultTestData))]
        public async Task Validate_WithValidCommand_PassesValidation(CreateVariantTypesClassCommand testCommand)
        {
            // Arrange
            var validator = GetValidationBehaviour();
            var expectedId = new Fixture().Create<System.Guid>();
            // Act
            var result = await validator.Handle(testCommand, CancellationToken.None, () => Task.FromResult(expectedId));

            // Assert
            result.Should().Be(expectedId);
        }

        public static IEnumerable<object[]> GetFailedResultTestData()
        {
            var fixture = new Fixture();
            fixture.Customize<CreateVariantTypesClassCommand>(comp => comp.With(x => x.StrCollection, () => default));
            var testCommand = fixture.Create<CreateVariantTypesClassCommand>();
            yield return new object[] { testCommand, "StrCollection", "not be empty" };

            fixture = new Fixture();
            fixture.Customize<CreateVariantTypesClassCommand>(comp => comp.With(x => x.IntCollection, () => default));
            testCommand = fixture.Create<CreateVariantTypesClassCommand>();
            yield return new object[] { testCommand, "IntCollection", "not be empty" };
        }

        [Theory]
        [MemberData(nameof(GetFailedResultTestData))]
        public async Task Validate_WithInvalidCommand_FailsValidation(CreateVariantTypesClassCommand testCommand, string expectedPropertyName, string expectedPhrase)
        {
            // Arrange
            var validator = GetValidationBehaviour();
            var expectedId = new Fixture().Create<System.Guid>();
            // Act
            var act = async () => await validator.Handle(testCommand, CancellationToken.None, () => Task.FromResult(expectedId));

            // Assert
            act.Should().ThrowAsync<ValidationException>().Result
            .Which.Errors.Should().Contain(x => x.PropertyName == expectedPropertyName && x.ErrorMessage.Contains(expectedPhrase));
        }

        private ValidationBehaviour<CreateVariantTypesClassCommand, System.Guid> GetValidationBehaviour()
        {
            return new ValidationBehaviour<CreateVariantTypesClassCommand, System.Guid>(new[] { new CreateVariantTypesClassCommandValidator() });
        }
    }
}