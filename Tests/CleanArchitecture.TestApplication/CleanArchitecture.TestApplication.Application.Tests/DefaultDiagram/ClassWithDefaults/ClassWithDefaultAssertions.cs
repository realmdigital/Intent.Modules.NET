using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.CRUD.Tests.Assertions.AssertionClass", Version = "1.0")]

namespace CleanArchitecture.TestApplication.Application.Tests.DefaultDiagram.ClassWithDefaults
{
    public static class ClassWithDefaultAssertions
    {
    }
}