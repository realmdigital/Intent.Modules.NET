using System;
using Entities.PrivateSetters.TestApplication.Application.Common.Interfaces;
using Intent.RoslynWeaver.Attributes;
using MediatR;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.CommandModels", Version = "1.0")]

namespace Entities.PrivateSetters.TestApplication.Application.OneToManySources.CreateOneToManySource
{
    public class CreateOneToManySourceCommand : IRequest<Guid>, ICommand
    {
        public CreateOneToManySourceCommand(string attribute)
        {
            Attribute = attribute;
        }

        public string Attribute { get; set; }
    }
}