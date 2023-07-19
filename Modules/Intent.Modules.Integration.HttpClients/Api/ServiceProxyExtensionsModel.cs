using System;
using System.Collections.Generic;
using System.Linq;
using Intent.Metadata.Models;
using Intent.Modelers.Types.ServiceProxies.Api;
using Intent.Modules.Common;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.ModuleBuilder.Templates.Api.ApiElementExtensionModel", Version = "1.0")]

namespace Intent.Integration.HttpClients.Api
{
    [IntentManaged(Mode.Fully, Signature = Mode.Fully)]
    public class ServiceProxyExtensionsModel : ServiceProxyModel
    {
        [IntentManaged(Mode.Fully, Body = Mode.Ignore)]
        public ServiceProxyExtensionsModel(IElement element) : base(element)
        {
        }

    }
}