using System;
using AdvancedMappingCrud.Repositories.Tests.Application.Common;
using AdvancedMappingCrud.Repositories.Tests.Application.Common.Interfaces;
using Intent.RoslynWeaver.Attributes;
using MediatR;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.QueryModels", Version = "1.0")]

namespace AdvancedMappingCrud.Repositories.Tests.Application.FileUploads.SimpleDownload
{
    public class SimpleDownloadQuery : IRequest<SimpleFileDownloadDto>, IQuery
    {
        public SimpleDownloadQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}