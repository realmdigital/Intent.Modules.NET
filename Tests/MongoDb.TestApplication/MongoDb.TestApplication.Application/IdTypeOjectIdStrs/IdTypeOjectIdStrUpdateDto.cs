using System;
using System.Collections.Generic;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.Dtos.DtoModel", Version = "1.0")]

namespace MongoDb.TestApplication.Application.IdTypeOjectIdStrs
{
    public class IdTypeOjectIdStrUpdateDto
    {
        public IdTypeOjectIdStrUpdateDto()
        {
        }

        public string Id { get; set; } = null!;
        public string Attribute { get; set; } = null!;

        public static IdTypeOjectIdStrUpdateDto Create(string id, string attribute)
        {
            return new IdTypeOjectIdStrUpdateDto
            {
                Id = id,
                Attribute = attribute
            };
        }
    }
}