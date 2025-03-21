using System;
using System.Collections.Generic;
using System.Linq;
using Intent.Metadata.Models;
using Intent.Modelers.Domain.Api;
using Intent.Modules.Common;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.ModuleBuilder.Templates.Api.ApiElementModelExtensions", Version = "1.0")]

namespace Intent.Redis.Om.Repositories.Api
{
    public static class AssociationTargetEndModelStereotypeExtensions
    {
        public static FieldSetting GetFieldSetting(this AssociationTargetEndModel model)
        {
            var stereotype = model.GetStereotype(FieldSetting.DefinitionId);
            return stereotype != null ? new FieldSetting(stereotype) : null;
        }


        public static bool HasFieldSetting(this AssociationTargetEndModel model)
        {
            return model.HasStereotype(FieldSetting.DefinitionId);
        }

        public static bool TryGetFieldSetting(this AssociationTargetEndModel model, out FieldSetting stereotype)
        {
            if (!HasFieldSetting(model))
            {
                stereotype = null;
                return false;
            }

            stereotype = new FieldSetting(model.GetStereotype(FieldSetting.DefinitionId));
            return true;
        }

        public static Indexed GetIndexed(this AssociationTargetEndModel model)
        {
            var stereotype = model.GetStereotype(Indexed.DefinitionId);
            return stereotype != null ? new Indexed(stereotype) : null;
        }


        public static bool HasIndexed(this AssociationTargetEndModel model)
        {
            return model.HasStereotype(Indexed.DefinitionId);
        }

        public static bool TryGetIndexed(this AssociationTargetEndModel model, out Indexed stereotype)
        {
            if (!HasIndexed(model))
            {
                stereotype = null;
                return false;
            }

            stereotype = new Indexed(model.GetStereotype(Indexed.DefinitionId));
            return true;
        }

        public class FieldSetting
        {
            private IStereotype _stereotype;
            public const string DefinitionId = "b92797e6-52ec-464e-80d3-2274cc97a1a1";

            public FieldSetting(IStereotype stereotype)
            {
                _stereotype = stereotype;
            }

            public string StereotypeName => _stereotype.Name;

            public string Name()
            {
                return _stereotype.GetProperty<string>("Name");
            }

        }

        public class Indexed
        {
            private IStereotype _stereotype;
            public const string DefinitionId = "e1fc703d-389d-4241-aee1-ee6ce385bbc0";

            public Indexed(IStereotype stereotype)
            {
                _stereotype = stereotype;
            }

            public string Name => _stereotype.Name;

        }

    }
}