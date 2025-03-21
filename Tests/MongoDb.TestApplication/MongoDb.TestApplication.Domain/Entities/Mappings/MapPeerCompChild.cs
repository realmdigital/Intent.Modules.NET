using System;
using Intent.RoslynWeaver.Attributes;

[assembly: IntentTemplate("Intent.Entities.DomainEntity", Version = "2.0")]

namespace MongoDb.TestApplication.Domain.Entities.Mappings
{
    [DefaultIntentManaged(Mode.Fully, Targets = Targets.Methods, Body = Mode.Ignore, AccessModifiers = AccessModifiers.Public)]
    public class MapPeerCompChild
    {
        public MapPeerCompChild()
        {
            PeerCompChildAtt = null!;
            MapPeerCompChildAggId = null!;
        }

        public string PeerCompChildAtt { get; set; }

        public string MapPeerCompChildAggId { get; set; }
    }
}