// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 17.0.0.0
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------
namespace Intent.Modules.AspNetCore.Controllers.Templates.Controller
{
    using System.Collections.Generic;
    using System.Linq;
    using Intent.Modules.Common;
    using Intent.Modules.Common.Templates;
    using Intent.Modules.Common.CSharp.Templates;
    using Intent.Templates;
    using Intent.Metadata.Models;
    using Intent.Modelers.Services.Api;
    using System;
    
    /// <summary>
    /// Class to produce the template output
    /// </summary>
    
    #line 1 "C:\Dev\Intent.Modules.NET\Modules\Intent.Modules.AspNetCore.Controllers\Templates\Controller\ControllerTemplate.tt"
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "17.0.0.0")]
    public partial class ControllerTemplate : CSharpTemplateBase<Intent.Modelers.Services.Api.ServiceModel, Intent.Modules.AspNetCore.Controllers.Templates.Controller.ControllerDecorator>
    {
#line hidden
        /// <summary>
        /// Create the template output
        /// </summary>
        public override string TransformText()
        {
            this.Write(@"using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: DefaultIntentManaged(Mode.Fully, Targets = Targets.Usings)]

namespace ");
            
            #line 22 "C:\Dev\Intent.Modules.NET\Modules\Intent.Modules.AspNetCore.Controllers\Templates\Controller\ControllerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Namespace));
            
            #line default
            #line hidden
            this.Write("\r\n{\r\n    [ApiController]\r\n    ");
            
            #line 25 "C:\Dev\Intent.Modules.NET\Modules\Intent.Modules.AspNetCore.Controllers\Templates\Controller\ControllerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(GetControllerAttributes()));
            
            #line default
            #line hidden
            this.Write("\r\n    public class ");
            
            #line 26 "C:\Dev\Intent.Modules.NET\Modules\Intent.Modules.AspNetCore.Controllers\Templates\Controller\ControllerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(ClassName));
            
            #line default
            #line hidden
            this.Write(" : ");
            
            #line 26 "C:\Dev\Intent.Modules.NET\Modules\Intent.Modules.AspNetCore.Controllers\Templates\Controller\ControllerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(GetControllerBase()));
            
            #line default
            #line hidden
            this.Write("\r\n    {");
            
            #line 27 "C:\Dev\Intent.Modules.NET\Modules\Intent.Modules.AspNetCore.Controllers\Templates\Controller\ControllerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(GetEnterClass()));
            
            #line default
            #line hidden
            this.Write("\r\n\r\n        public ");
            
            #line 29 "C:\Dev\Intent.Modules.NET\Modules\Intent.Modules.AspNetCore.Controllers\Templates\Controller\ControllerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(ClassName));
            
            #line default
            #line hidden
            this.Write("(");
            
            #line 29 "C:\Dev\Intent.Modules.NET\Modules\Intent.Modules.AspNetCore.Controllers\Templates\Controller\ControllerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(GetConstructorParameters()));
            
            #line default
            #line hidden
            this.Write(")\r\n        {");
            
            #line 30 "C:\Dev\Intent.Modules.NET\Modules\Intent.Modules.AspNetCore.Controllers\Templates\Controller\ControllerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(GetConstructorImplementation()));
            
            #line default
            #line hidden
            this.Write("\r\n        }\r\n\r\n");
            
            #line 33 "C:\Dev\Intent.Modules.NET\Modules\Intent.Modules.AspNetCore.Controllers\Templates\Controller\ControllerTemplate.tt"
  foreach (var operation in Model.Operations)
    {
            
            #line default
            #line hidden
            this.Write("        ");
            
            #line 35 "C:\Dev\Intent.Modules.NET\Modules\Intent.Modules.AspNetCore.Controllers\Templates\Controller\ControllerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(GetOperationComments(operation)));
            
            #line default
            #line hidden
            this.Write("\r\n        ");
            
            #line 36 "C:\Dev\Intent.Modules.NET\Modules\Intent.Modules.AspNetCore.Controllers\Templates\Controller\ControllerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(GetOperationAttributes(operation)));
            
            #line default
            #line hidden
            this.Write("\r\n        public async Task<");
            
            #line 37 "C:\Dev\Intent.Modules.NET\Modules\Intent.Modules.AspNetCore.Controllers\Templates\Controller\ControllerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(GetReturnType(operation)));
            
            #line default
            #line hidden
            this.Write("> ");
            
            #line 37 "C:\Dev\Intent.Modules.NET\Modules\Intent.Modules.AspNetCore.Controllers\Templates\Controller\ControllerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(operation.Name));
            
            #line default
            #line hidden
            this.Write("(");
            
            #line 37 "C:\Dev\Intent.Modules.NET\Modules\Intent.Modules.AspNetCore.Controllers\Templates\Controller\ControllerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(GetOperationParameters(operation)));
            
            #line default
            #line hidden
            this.Write(")\r\n        {");
            
            #line 38 "C:\Dev\Intent.Modules.NET\Modules\Intent.Modules.AspNetCore.Controllers\Templates\Controller\ControllerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(GetEnterOperationBody(operation)));
            
            #line default
            #line hidden
            this.Write("\r\n            ");
            
            #line 39 "C:\Dev\Intent.Modules.NET\Modules\Intent.Modules.AspNetCore.Controllers\Templates\Controller\ControllerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(GetMidOperationBody(operation)));
            
            #line default
            #line hidden
            this.Write("\r\n            ");
            
            #line 40 "C:\Dev\Intent.Modules.NET\Modules\Intent.Modules.AspNetCore.Controllers\Templates\Controller\ControllerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(GetExitOperationBody(operation)));
            
            #line default
            #line hidden
            this.Write("\r\n        }\r\n\r\n");
            
            #line 43 "C:\Dev\Intent.Modules.NET\Modules\Intent.Modules.AspNetCore.Controllers\Templates\Controller\ControllerTemplate.tt"
  }
            
            #line default
            #line hidden
            
            #line 43 "C:\Dev\Intent.Modules.NET\Modules\Intent.Modules.AspNetCore.Controllers\Templates\Controller\ControllerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(GetExitClass()));
            
            #line default
            #line hidden
            this.Write("\r\n    }\r\n}");
            return this.GenerationEnvironment.ToString();
        }
    }
    
    #line default
    #line hidden
}
