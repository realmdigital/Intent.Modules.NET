using Intent.RoslynWeaver.Attributes;
using MassTransitFinbuckle.Test.Application.Common.Interfaces;
using MediatR;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.CommandModels", Version = "1.0")]

namespace MassTransitFinbuckle.Test.Application.RequestResponse.Test
{
    public class TestCommand : IRequest, ICommand
    {
        public TestCommand(string value)
        {
            Value = value;
        }

        public string Value { get; set; }
    }
}