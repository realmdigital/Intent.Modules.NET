using System;
using AdvancedMappingCrud.Repositories.Tests.Application.Common.Interfaces;
using Intent.RoslynWeaver.Attributes;
using MediatR;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.CommandModels", Version = "1.0")]

namespace AdvancedMappingCrud.Repositories.Tests.Application.ParentWithAnemicChildren.UpdateParentWithAnemicChild
{
    public class UpdateParentWithAnemicChildCommand : IRequest, ICommand
    {
        public UpdateParentWithAnemicChildCommand(string name, string surname, Guid id)
        {
            Name = name;
            Surname = surname;
            Id = id;
        }

        public string Name { get; set; }
        public string Surname { get; set; }
        public Guid Id { get; set; }
    }
}