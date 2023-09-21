using System.Collections.Generic;
using System.Linq;
using CleanArchitecture.TestApplication.Application.UniqueIndexConstraint.AggregateWithUniqueConstraintIndexElements;
using CleanArchitecture.TestApplication.Application.UniqueIndexConstraint.AggregateWithUniqueConstraintIndexElements.CreateAggregateWithUniqueConstraintIndexElement;
using CleanArchitecture.TestApplication.Application.UniqueIndexConstraint.AggregateWithUniqueConstraintIndexElements.UpdateAggregateWithUniqueConstraintIndexElement;
using CleanArchitecture.TestApplication.Domain.Entities.UniqueIndexConstraint;
using FluentAssertions;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.CRUD.Tests.Assertions.AssertionClass", Version = "1.0")]

namespace CleanArchitecture.TestApplication.Application.Tests.UniqueIndexConstraint.AggregateWithUniqueConstraintIndexElements
{
    public static class AggregateWithUniqueConstraintIndexElementAssertions
    {
        public static void AssertEquivalent(
            CreateAggregateWithUniqueConstraintIndexElementCommand expectedDto,
            AggregateWithUniqueConstraintIndexElement actualEntity)
        {
            if (expectedDto == null)
            {
                actualEntity.Should().BeNull();
                return;
            }

            actualEntity.Should().NotBeNull();
            actualEntity.SingleUniqueField.Should().Be(expectedDto.SingleUniqueField);
            actualEntity.CompUniqueFieldA.Should().Be(expectedDto.CompUniqueFieldA);
            actualEntity.CompUniqueFieldB.Should().Be(expectedDto.CompUniqueFieldB);
        }

        public static void AssertEquivalent(
            IEnumerable<AggregateWithUniqueConstraintIndexElementDto> actualDtos,
            IEnumerable<AggregateWithUniqueConstraintIndexElement> expectedEntities)
        {
            if (expectedEntities == null)
            {
                actualDtos.Should().BeNullOrEmpty();
                return;
            }

            actualDtos.Should().HaveSameCount(actualDtos);
            for (int i = 0; i < expectedEntities.Count(); i++)
            {
                var entity = expectedEntities.ElementAt(i);
                var dto = actualDtos.ElementAt(i);
                if (entity == null)
                {
                    dto.Should().BeNull();
                    continue;
                }

                dto.Should().NotBeNull();
                dto.Id.Should().Be(entity.Id);
                dto.SingleUniqueField.Should().Be(entity.SingleUniqueField);
                dto.CompUniqueFieldA.Should().Be(entity.CompUniqueFieldA);
                dto.CompUniqueFieldB.Should().Be(entity.CompUniqueFieldB);
            }
        }

        public static void AssertEquivalent(
            AggregateWithUniqueConstraintIndexElementDto actualDto,
            AggregateWithUniqueConstraintIndexElement expectedEntity)
        {
            if (expectedEntity == null)
            {
                actualDto.Should().BeNull();
                return;
            }

            actualDto.Should().NotBeNull();
            actualDto.Id.Should().Be(expectedEntity.Id);
            actualDto.SingleUniqueField.Should().Be(expectedEntity.SingleUniqueField);
            actualDto.CompUniqueFieldA.Should().Be(expectedEntity.CompUniqueFieldA);
            actualDto.CompUniqueFieldB.Should().Be(expectedEntity.CompUniqueFieldB);
        }

        public static void AssertEquivalent(
            UpdateAggregateWithUniqueConstraintIndexElementCommand expectedDto,
            AggregateWithUniqueConstraintIndexElement actualEntity)
        {
            if (expectedDto == null)
            {
                actualEntity.Should().BeNull();
                return;
            }

            actualEntity.Should().NotBeNull();
            actualEntity.SingleUniqueField.Should().Be(expectedDto.SingleUniqueField);
            actualEntity.CompUniqueFieldA.Should().Be(expectedDto.CompUniqueFieldA);
            actualEntity.CompUniqueFieldB.Should().Be(expectedDto.CompUniqueFieldB);
        }
    }
}