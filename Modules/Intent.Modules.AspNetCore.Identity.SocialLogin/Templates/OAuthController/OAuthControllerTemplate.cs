﻿// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 16.0.0.0
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------
namespace Intent.Modules.AspNetCore.Identity.SocialLogin.Templates.OAuthController
{
    using System.Collections.Generic;
    using System.Linq;
    using Intent.Modules.Common;
    using Intent.Modules.Common.Templates;
    using Intent.Modules.Common.CSharp.Templates;
    using Intent.Templates;
    using Intent.Metadata.Models;
    using Intent.Modules.AspNetCore.Identity.SocialLogin.Templates;
    using Intent.Modules.AspNetCore.Identity.AccountController.Templates;
    using System;
    
    /// <summary>
    /// Class to produce the template output
    /// </summary>
    
    #line 1 "/home/shaine/projects/intent-architect/Intent.Modules.NET/Modules/Intent.Modules.AspNetCore.Identity.SocialLogin/Templates/OAuthController/OAuthControllerTemplate.tt"
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "16.0.0.0")]
    public partial class OAuthControllerTemplate : CSharpTemplateBase<object>
    {
#line hidden
        /// <summary>
        /// Create the template output
        /// </summary>
        public override string TransformText()
        {
            this.Write("using System;\nusing System.Collections.Generic;\nusing System.Linq;\nusing System.Security.Claims;\nusing System.Text;\nusing System.Text.Json;\nusing System.Threading;\nusing System.Threading.Tasks;\nusing Microsoft.AspNetCore.Authorization;\nusing Microsoft.AspNetCore.Http;\nusing Microsoft.AspNetCore.Identity;\nusing Microsoft.AspNetCore.Mvc;\nusing Microsoft.AspNetCore.Mvc.ModelBinding;\nusing Microsoft.AspNetCore.WebUtilities;\nusing Microsoft.Extensions.Logging;\n\n[assembly: DefaultIntentManaged(Mode.Fully)]\n\nnamespace ");
            
            #line 30 "/home/shaine/projects/intent-architect/Intent.Modules.NET/Modules/Intent.Modules.AspNetCore.Identity.SocialLogin/Templates/OAuthController/OAuthControllerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Namespace));
            
            #line default
            #line hidden
            this.Write("\n{\n    [Route(\"api/oauth\")]\n    [ApiController]\n    public class ");
            
            #line 34 "/home/shaine/projects/intent-architect/Intent.Modules.NET/Modules/Intent.Modules.AspNetCore.Identity.SocialLogin/Templates/OAuthController/OAuthControllerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(ClassName));
            
            #line default
            #line hidden
            this.Write(" : ControllerBase\n    {\n        private readonly SignInManager<");
            
            #line 36 "/home/shaine/projects/intent-architect/Intent.Modules.NET/Modules/Intent.Modules.AspNetCore.Identity.SocialLogin/Templates/OAuthController/OAuthControllerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.GetIdentityUserClass()));
            
            #line default
            #line hidden
            this.Write("> _signInManager;\n        private readonly UserManager<");
            
            #line 37 "/home/shaine/projects/intent-architect/Intent.Modules.NET/Modules/Intent.Modules.AspNetCore.Identity.SocialLogin/Templates/OAuthController/OAuthControllerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.GetIdentityUserClass()));
            
            #line default
            #line hidden
            this.Write("> _userManager;\n        private readonly ");
            
            #line 38 "/home/shaine/projects/intent-architect/Intent.Modules.NET/Modules/Intent.Modules.AspNetCore.Identity.SocialLogin/Templates/OAuthController/OAuthControllerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.GetTokenServiceInterfaceName()));
            
            #line default
            #line hidden
            this.Write(" _tokenService;\n        private readonly ILogger<");
            
            #line 39 "/home/shaine/projects/intent-architect/Intent.Modules.NET/Modules/Intent.Modules.AspNetCore.Identity.SocialLogin/Templates/OAuthController/OAuthControllerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(ClassName));
            
            #line default
            #line hidden
            this.Write("> _logger;\n\n        public ");
            
            #line 41 "/home/shaine/projects/intent-architect/Intent.Modules.NET/Modules/Intent.Modules.AspNetCore.Identity.SocialLogin/Templates/OAuthController/OAuthControllerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(ClassName));
            
            #line default
            #line hidden
            this.Write("(\n            SignInManager<");
            
            #line 42 "/home/shaine/projects/intent-architect/Intent.Modules.NET/Modules/Intent.Modules.AspNetCore.Identity.SocialLogin/Templates/OAuthController/OAuthControllerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.GetIdentityUserClass()));
            
            #line default
            #line hidden
            this.Write("> signInManager,\n            UserManager<");
            
            #line 43 "/home/shaine/projects/intent-architect/Intent.Modules.NET/Modules/Intent.Modules.AspNetCore.Identity.SocialLogin/Templates/OAuthController/OAuthControllerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.GetIdentityUserClass()));
            
            #line default
            #line hidden
            this.Write("> userManager,\n            ");
            
            #line 44 "/home/shaine/projects/intent-architect/Intent.Modules.NET/Modules/Intent.Modules.AspNetCore.Identity.SocialLogin/Templates/OAuthController/OAuthControllerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.GetTokenServiceInterfaceName()));
            
            #line default
            #line hidden
            this.Write(" tokenService,\n            ILogger<");
            
            #line 45 "/home/shaine/projects/intent-architect/Intent.Modules.NET/Modules/Intent.Modules.AspNetCore.Identity.SocialLogin/Templates/OAuthController/OAuthControllerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(ClassName));
            
            #line default
            #line hidden
            this.Write("> logger)\n        {\n            _signInManager = signInManager;\n            _userManager = userManager;\n            _tokenService = tokenService;\n            _logger = logger;\n        }\n\n        [HttpGet(\"providers\", Name=\"GetProviders\")]\n        [AllowAnonymous]\n        public async Task<ActionResult<List<string>>> GetProviders()\n        {\n            var providers = (await _signInManager.GetExternalAuthenticationSchemesAsync()).Select(x => x.Name).ToList();\n\n            return Ok(providers);\n        }\n\n        [HttpPost]\n        [AllowAnonymous]\n        public async Task<ChallengeResult> StartChallenge(string provider,\n            string? returnUrl /* even though this is not used here, it does actually end up in the callback */\n        )\n        {\n            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, null);\n            properties.AllowRefresh = true;\n\n            return Challenge(properties, new[] { provider });\n        }\n\n        [HttpGet]\n        [AllowAnonymous]\n        public async Task<IActionResult> Callback(string? returnUrl, string? remoteError)\n        {\n            var info = await _signInManager.GetExternalLoginInfoAsync();\n            if (info is null)\n            {\n                return BadRequest(\"Error loading external login information.\");\n            }\n\n            // Duplicated from AccountController.Login START\n            \n            var email = \"shaineg@gmail.com\";\n            var password = \"Pass1234!\";\n\n            var user = await _userManager.FindByEmailAsync(email);\n            if (user == null ||\n                !await _userManager.CheckPasswordAsync(user, password))\n            {\n                _logger.LogWarning(\"Invalid login attempt.\");\n                return Forbid();\n            }\n\n            if (await _userManager.IsLockedOutAsync(user))\n            {\n                _logger.LogWarning(\"User account locked out.\");\n                return Forbid();\n            }\n\n            var claims = await _userManager.GetClaimsAsync(user);\n            var roles = await _userManager.GetRolesAsync(user);\n            foreach (var role in roles)\n            {\n                claims.Add(new Claim(\"role\", role));\n            }\n\n            var accessToken = _tokenService.GenerateAccessToken(username: email, claims: claims.ToArray());\n            var newRefreshToken = _tokenService.GenerateRefreshToken();\n\n            user.RefreshToken = newRefreshToken.Token;\n            user.RefreshTokenExpired = newRefreshToken.Expiry;\n            await _userManager.UpdateAsync(user);\n\n            _logger.LogInformation(\"User logged in.\");\n\n            var token = new TokenResultDto\n            {\n                AuthenticationToken = accessToken,\n                RefreshToken = newRefreshToken.Token,\n            };\n            \n            // Duplicated from AccountController.Login END\n            \n            var options = new CookieOptions()\n            {\n                //Needed so that domain.com can access  the cookie set by api.domain.com\n                //Domain = settings.AppDomain,\n                Expires = DateTime.UtcNow.AddMinutes(1),\n            };\n\n            Response.Cookies.Append(\n                \"OurCookieName\", // <-- Key\n                JsonSerializer.Serialize(token)\n                /*Common.IdentityConstants.AuthTokenHolderCookieName,\n                JsonConvert.SerializeObject(result, new JsonSerializerSettings\n                {\n                    ContractResolver = new DefaultContractResolver\n                    {\n                        NamingStrategy = new CamelCaseNamingStrategy()\n                    },\n                    Formatting = Formatting.Indented\n                })*/, options);\n            \n            return Ok();\n        }\n\n    }\n}");
            return this.GenerationEnvironment.ToString();
        }
    }
    
}
