﻿// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 16.0.0.0
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------
namespace Intent.Modules.Eventing.MassTransit.Templates.Consumer
{
    using Intent.Modelers.Eventing.Api;
    using Intent.Modules.Common.CSharp.Templates;
    using System;
    
    /// <summary>
    /// Class to produce the template output
    /// </summary>
    
    #line 1 "C:\Dev\Intent.Modules.NET\Modules\Intent.Modules.Eventing.MassTransit\Templates\Consumer\ConsumerTemplate.tt"
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "16.0.0.0")]
    public partial class ConsumerTemplate : CSharpTemplateBase<Intent.Modelers.Eventing.Api.MessageHandlerModel>
    {
#line hidden
        /// <summary>
        /// Create the template output
        /// </summary>
        public override string TransformText()
        {
            this.Write("using System;\r\nusing System.Threading.Tasks;\r\nusing MassTransit;\r\n\r\n[assembly: DefaultIntentManaged(Mode.Fully)]\r\n\r\nnamespace ");
            
            #line 10 "C:\Dev\Intent.Modules.NET\Modules\Intent.Modules.Eventing.MassTransit\Templates\Consumer\ConsumerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Namespace));
            
            #line default
            #line hidden
            this.Write("\r\n{\r\n    [IntentManaged(Mode.Merge)]\r\n    public class ");
            
            #line 13 "C:\Dev\Intent.Modules.NET\Modules\Intent.Modules.Eventing.MassTransit\Templates\Consumer\ConsumerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(ClassName));
            
            #line default
            #line hidden
            this.Write(" : IConsumer<");
            
            #line 13 "C:\Dev\Intent.Modules.NET\Modules\Intent.Modules.Eventing.MassTransit\Templates\Consumer\ConsumerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(GetMessageName()));
            
            #line default
            #line hidden
            this.Write(">\r\n    {\r\n        [IntentManaged(Mode.Ignore)]\r\n        public ");
            
            #line 16 "C:\Dev\Intent.Modules.NET\Modules\Intent.Modules.Eventing.MassTransit\Templates\Consumer\ConsumerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(ClassName));
            
            #line default
            #line hidden
            this.Write("()\r\n        {            \r\n        }\r\n        \r\n        [IntentManaged(Mode.Fully, Body = Mode.Ignore)]\r\n        public async Task Consume(ConsumeContext<");
            
            #line 21 "C:\Dev\Intent.Modules.NET\Modules\Intent.Modules.Eventing.MassTransit\Templates\Consumer\ConsumerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(GetMessageName()));
            
            #line default
            #line hidden
            this.Write("> context)\r\n        {\r\n            throw new NotImplementedException();\r\n        }\r\n    }\r\n}");
            return this.GenerationEnvironment.ToString();
        }
    }
    
}
