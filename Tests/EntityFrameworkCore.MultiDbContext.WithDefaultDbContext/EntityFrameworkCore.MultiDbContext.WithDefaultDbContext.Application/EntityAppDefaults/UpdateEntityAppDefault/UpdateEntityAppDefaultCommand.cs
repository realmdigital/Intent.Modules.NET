using System;
using EntityFrameworkCore.MultiDbContext.WithDefaultDbContext.Application.Common.Interfaces;
using Intent.RoslynWeaver.Attributes;
using MediatR;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.CommandModels", Version = "1.0")]

namespace EntityFrameworkCore.MultiDbContext.WithDefaultDbContext.Application.EntityAppDefaults.UpdateEntityAppDefault
{
    public class UpdateEntityAppDefaultCommand : IRequest, ICommand
    {
        public UpdateEntityAppDefaultCommand(Guid id, string message)
        {
            Id = id;
            Message = message;
        }

        public Guid Id { get; set; }
        public string Message { get; set; }
    }
}