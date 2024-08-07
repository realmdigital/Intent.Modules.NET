// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 17.0.0.0
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------
namespace Intent.Modules.Application.MediatR.Behaviours.Templates.LoggingBehaviour
{
    using System.Collections.Generic;
    using System.Linq;
    using Intent.Modules.Common;
    using Intent.Modules.Common.Templates;
    using Intent.Modules.Common.CSharp.Templates;
    using Intent.Templates;
    using Intent.Metadata.Models;
    using System;
    
    /// <summary>
    /// Class to produce the template output
    /// </summary>
    
    #line 1 "C:\Dev\Intent.Modules.NET\Modules\Intent.Modules.Application.MediatR.Behaviours\Templates\LoggingBehaviour\LoggingBehaviourTemplate.tt"
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "17.0.0.0")]
    public partial class LoggingBehaviourTemplate : CSharpTemplateBase<object>
    {
#line hidden
        /// <summary>
        /// Create the template output
        /// </summary>
        public override string TransformText()
        {
            this.Write("using MediatR.Pipeline;\r\nusing Microsoft.Extensions.Logging;\r\nusing System.Thread" +
                    "ing;\r\nusing System.Threading.Tasks;\r\n\r\n[assembly: DefaultIntentManaged(Mode.Full" +
                    "y)]\r\n\r\nnamespace ");
            
            #line 17 "C:\Dev\Intent.Modules.NET\Modules\Intent.Modules.Application.MediatR.Behaviours\Templates\LoggingBehaviour\LoggingBehaviourTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Namespace));
            
            #line default
            #line hidden
            this.Write("\r\n{\r\n    public class ");
            
            #line 19 "C:\Dev\Intent.Modules.NET\Modules\Intent.Modules.Application.MediatR.Behaviours\Templates\LoggingBehaviour\LoggingBehaviourTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(ClassName));
            
            #line default
            #line hidden
            this.Write("<TRequest> : IRequestPreProcessor<TRequest>\r\n        where TRequest : notnull\r\n  " +
                    "  {\r\n        private readonly ILogger _logger;\r\n        private readonly ");
            
            #line 23 "C:\Dev\Intent.Modules.NET\Modules\Intent.Modules.Application.MediatR.Behaviours\Templates\LoggingBehaviour\LoggingBehaviourTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.GetCurrentUserServiceInterface()));
            
            #line default
            #line hidden
            this.Write(" _currentUserService;\r\n\r\n        public ");
            
            #line 25 "C:\Dev\Intent.Modules.NET\Modules\Intent.Modules.Application.MediatR.Behaviours\Templates\LoggingBehaviour\LoggingBehaviourTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(ClassName));
            
            #line default
            #line hidden
            this.Write("(ILogger<TRequest> logger, ");
            
            #line 25 "C:\Dev\Intent.Modules.NET\Modules\Intent.Modules.Application.MediatR.Behaviours\Templates\LoggingBehaviour\LoggingBehaviourTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.GetCurrentUserServiceInterface()));
            
            #line default
            #line hidden
            this.Write(@" currentUserService)
        {
            _logger = logger;
            _currentUserService = currentUserService;
        }

        public Task Process(TRequest request, CancellationToken cancellationToken)
        {
            var requestName = typeof(TRequest).Name;
            var userId = _currentUserService.UserId;
            var userName = _currentUserService.UserName;

            _logger.LogInformation(""");
            
            #line 37 "C:\Dev\Intent.Modules.NET\Modules\Intent.Modules.Application.MediatR.Behaviours\Templates\LoggingBehaviour\LoggingBehaviourTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(ExecutionContext.GetApplicationConfig().Name));
            
            #line default
            #line hidden
            this.Write(" Request: {Name} {@UserId} {@UserName} {@Request}\",\r\n                requestName," +
                    " userId, userName, request);\r\n            return Task.CompletedTask;\r\n        }\r" +
                    "\n    }\r\n}");
            return this.GenerationEnvironment.ToString();
        }
    }
    
    #line default
    #line hidden
}
