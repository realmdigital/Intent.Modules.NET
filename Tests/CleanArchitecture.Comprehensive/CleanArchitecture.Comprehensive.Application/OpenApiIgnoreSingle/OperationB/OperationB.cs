using CleanArchitecture.Comprehensive.Application.Common.Interfaces;
using Intent.RoslynWeaver.Attributes;
using MediatR;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.CommandModels", Version = "1.0")]

namespace CleanArchitecture.Comprehensive.Application.OpenApiIgnoreSingle.OperationB
{
    public class OperationB : IRequest, ICommand
    {
        public OperationB()
        {
        }
    }
}