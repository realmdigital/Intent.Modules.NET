using System;
using AspNetCore.Controllers.Secured.Application.Common.Interfaces;
using AspNetCore.Controllers.Secured.Application.Common.Security;
using Intent.RoslynWeaver.Attributes;
using MediatR;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.CommandModels", Version = "1.0")]

namespace AspNetCore.Controllers.Secured.Application.Buyers.UpdateBuyer
{
    [Authorize(Roles = "Role1", Policy = "Policy1")]
    [Authorize(Roles = "Role2", Policy = "Policy2")]
    public class UpdateBuyerCommand : IRequest, ICommand
    {
        public UpdateBuyerCommand(string name, string surname, string email, Guid id)
        {
            Name = name;
            Surname = surname;
            Email = email;
            Id = id;
        }

        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public Guid Id { get; set; }
    }
}