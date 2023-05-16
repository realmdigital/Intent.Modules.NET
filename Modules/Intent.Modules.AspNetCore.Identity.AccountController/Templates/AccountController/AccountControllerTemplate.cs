﻿// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 16.0.0.0
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------
namespace Intent.Modules.AspNetCore.Identity.AccountController.Templates.AccountController
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
    
    #line 1 "C:\Dev\Intent.Modules.NET\Modules\Intent.Modules.AspNetCore.Identity.AccountController\Templates\AccountController\AccountControllerTemplate.tt"
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "16.0.0.0")]
    public partial class AccountControllerTemplate : CSharpTemplateBase<object>
    {
#line hidden
        /// <summary>
        /// Create the template output
        /// </summary>
        public override string TransformText()
        {
            this.Write("using System;\r\nusing System.Linq;\r\nusing System.Security.Claims;\r\nusing System.Text;\r\nusing System.Threading;\r\nusing System.Threading.Tasks;\r\nusing Microsoft.AspNetCore.Authorization;\r\nusing Microsoft.AspNetCore.Identity;\r\nusing Microsoft.AspNetCore.Mvc;\r\nusing Microsoft.AspNetCore.Mvc.ModelBinding;\r\nusing Microsoft.AspNetCore.WebUtilities;\r\nusing Microsoft.Extensions.Logging;\r\n\r\n[assembly: DefaultIntentManaged(Mode.Fully)]\r\n\r\nnamespace ");
            
            #line 25 "C:\Dev\Intent.Modules.NET\Modules\Intent.Modules.AspNetCore.Identity.AccountController\Templates\AccountController\AccountControllerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Namespace));
            
            #line default
            #line hidden
            this.Write("\r\n{\r\n    [Route(\"api/[controller]/[action]\")]\r\n    [ApiController]\r\n    public class ");
            
            #line 29 "C:\Dev\Intent.Modules.NET\Modules\Intent.Modules.AspNetCore.Identity.AccountController\Templates\AccountController\AccountControllerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(ClassName));
            
            #line default
            #line hidden
            this.Write(" : ControllerBase\r\n    {\r\n        private readonly SignInManager<");
            
            #line 31 "C:\Dev\Intent.Modules.NET\Modules\Intent.Modules.AspNetCore.Identity.AccountController\Templates\AccountController\AccountControllerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.GetIdentityUserClass()));
            
            #line default
            #line hidden
            this.Write("> _signInManager;\r\n        private readonly UserManager<");
            
            #line 32 "C:\Dev\Intent.Modules.NET\Modules\Intent.Modules.AspNetCore.Identity.AccountController\Templates\AccountController\AccountControllerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.GetIdentityUserClass()));
            
            #line default
            #line hidden
            this.Write("> _userManager;\r\n        private readonly IUserStore<");
            
            #line 33 "C:\Dev\Intent.Modules.NET\Modules\Intent.Modules.AspNetCore.Identity.AccountController\Templates\AccountController\AccountControllerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.GetIdentityUserClass()));
            
            #line default
            #line hidden
            this.Write("> _userStore;\r\n        private readonly ILogger<");
            
            #line 34 "C:\Dev\Intent.Modules.NET\Modules\Intent.Modules.AspNetCore.Identity.AccountController\Templates\AccountController\AccountControllerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(ClassName));
            
            #line default
            #line hidden
            this.Write("> _logger;\r\n        private readonly ");
            
            #line 35 "C:\Dev\Intent.Modules.NET\Modules\Intent.Modules.AspNetCore.Identity.AccountController\Templates\AccountController\AccountControllerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.GetAccountEmailSenderInterfaceName()));
            
            #line default
            #line hidden
            this.Write(" _accountEmailSender;\r\n        private readonly ");
            
            #line 36 "C:\Dev\Intent.Modules.NET\Modules\Intent.Modules.AspNetCore.Identity.AccountController\Templates\AccountController\AccountControllerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.GetTokenServiceInterfaceName()));
            
            #line default
            #line hidden
            this.Write(" _tokenService;\r\n\r\n        public ");
            
            #line 38 "C:\Dev\Intent.Modules.NET\Modules\Intent.Modules.AspNetCore.Identity.AccountController\Templates\AccountController\AccountControllerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(ClassName));
            
            #line default
            #line hidden
            this.Write("(\r\n            SignInManager<");
            
            #line 39 "C:\Dev\Intent.Modules.NET\Modules\Intent.Modules.AspNetCore.Identity.AccountController\Templates\AccountController\AccountControllerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.GetIdentityUserClass()));
            
            #line default
            #line hidden
            this.Write("> signInManager,\r\n            IUserStore<");
            
            #line 40 "C:\Dev\Intent.Modules.NET\Modules\Intent.Modules.AspNetCore.Identity.AccountController\Templates\AccountController\AccountControllerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.GetIdentityUserClass()));
            
            #line default
            #line hidden
            this.Write("> userStore,\r\n            UserManager<");
            
            #line 41 "C:\Dev\Intent.Modules.NET\Modules\Intent.Modules.AspNetCore.Identity.AccountController\Templates\AccountController\AccountControllerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.GetIdentityUserClass()));
            
            #line default
            #line hidden
            this.Write("> userManager,\r\n            ILogger<");
            
            #line 42 "C:\Dev\Intent.Modules.NET\Modules\Intent.Modules.AspNetCore.Identity.AccountController\Templates\AccountController\AccountControllerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(ClassName));
            
            #line default
            #line hidden
            this.Write("> logger,\r\n            ");
            
            #line 43 "C:\Dev\Intent.Modules.NET\Modules\Intent.Modules.AspNetCore.Identity.AccountController\Templates\AccountController\AccountControllerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.GetAccountEmailSenderInterfaceName()));
            
            #line default
            #line hidden
            this.Write(" accountEmailSender,\r\n            ");
            
            #line 44 "C:\Dev\Intent.Modules.NET\Modules\Intent.Modules.AspNetCore.Identity.AccountController\Templates\AccountController\AccountControllerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.GetTokenServiceInterfaceName()));
            
            #line default
            #line hidden
            this.Write(" tokenService)\r\n        {\r\n            _signInManager = signInManager;\r\n            _userStore = userStore;\r\n            _userManager = userManager;\r\n            _logger = logger;\r\n            _accountEmailSender = accountEmailSender;\r\n            _tokenService = tokenService;\r\n        }\r\n\r\n        [HttpPost]\r\n        [AllowAnonymous]\r\n        public async Task<IActionResult> Register(RegisterDto input)\r\n        {\r\n            if (string.IsNullOrWhiteSpace(input.Email))\r\n            {\r\n                ModelState.AddModelError<RegisterDto>(x => x.Email, \"Mandatory\");\r\n            }\r\n\r\n            if (string.IsNullOrWhiteSpace(input.Password))\r\n            {\r\n                ModelState.AddModelError<RegisterDto>(x => x.Password, \"Mandatory\");\r\n            }\r\n\r\n            if (!ModelState.IsValid)\r\n            {\r\n                return BadRequest(ModelState);\r\n            }\r\n\r\n            var user = new ");
            
            #line 73 "C:\Dev\Intent.Modules.NET\Modules\Intent.Modules.AspNetCore.Identity.AccountController\Templates\AccountController\AccountControllerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.GetIdentityUserClass()));
            
            #line default
            #line hidden
            this.Write("();\r\n\r\n            await _userStore.SetUserNameAsync(user, input.Email, CancellationToken.None);\r\n            await _userManager.SetEmailAsync(user, input.Email);\r\n            var result = await _userManager.CreateAsync(user, input.Password!);\r\n\r\n            if (!result.Succeeded)\r\n            {\r\n                foreach (var error in result.Errors)\r\n                {\r\n                    ModelState.AddModelError(string.Empty, error.Description);\r\n                }\r\n\r\n                return BadRequest(ModelState);\r\n            }\r\n\r\n            _logger.LogInformation(\"User created a new account with password.\");\r\n\r\n            var userId = await _userManager.GetUserIdAsync(user);\r\n            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);\r\n            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));\r\n\r\n            if (_userManager.Options.SignIn.RequireConfirmedAccount)\r\n            {\r\n                await _accountEmailSender.SendEmailConfirmationRequest(\r\n                    email: input.Email!,\r\n                    userId: userId,\r\n                    code: code);\r\n            }\r\n\r\n            return Ok();\r\n        }\r\n\r\n        [HttpPost]\r\n        [AllowAnonymous]\r\n        public async Task<ActionResult<TokenResultDto>> Login(LoginDto input)\r\n        {\r\n            if (string.IsNullOrWhiteSpace(input.Email))\r\n            {\r\n                ModelState.AddModelError<LoginDto>(x => x.Email, \"Mandatory\");\r\n            }\r\n\r\n            if (string.IsNullOrWhiteSpace(input.Password))\r\n            {\r\n                ModelState.AddModelError<LoginDto>(x => x.Password, \"Mandatory\");\r\n            }\r\n\r\n            if (!ModelState.IsValid)\r\n            {\r\n                return BadRequest(ModelState);\r\n            }\r\n\r\n            var email = input.Email!;\r\n            var password = input.Password!;\r\n\r\n            var user = await _userManager.FindByEmailAsync(email);\r\n            if (user == null ||\r\n                !await _userManager.CheckPasswordAsync(user, password))\r\n            {\r\n                _logger.LogWarning(\"Invalid login attempt.\");\r\n                return Forbid();\r\n            }\r\n\r\n            if (await _userManager.IsLockedOutAsync(user))\r\n            {\r\n                _logger.LogWarning(\"User account locked out.\");\r\n                return Forbid();\r\n            }\r\n\r\n            var claims = await _userManager.GetClaimsAsync(user);\r\n            var roles = await _userManager.GetRolesAsync(user);\r\n            foreach (var role in roles)\r\n            {\r\n                claims.Add(new Claim(\"role\", role));\r\n            }\r\n\r\n            var token = _tokenService.GenerateAccessToken(username: email, claims: claims.ToArray());\r\n            var newRefreshToken = _tokenService.GenerateRefreshToken();\r\n\r\n            user.RefreshToken = newRefreshToken.Token;\r\n            user.RefreshTokenExpired = newRefreshToken.Expiry;\r\n            await _userManager.UpdateAsync(user);\r\n            \r\n            _logger.LogInformation(\"User logged in.\");\r\n            \r\n            return Ok(new TokenResultDto\r\n            {\r\n                AuthenticationToken = token,\r\n                RefreshToken = newRefreshToken.Token\r\n            });\r\n        }\r\n\r\n        [HttpPost]\r\n        [AllowAnonymous]\r\n        public async Task<ActionResult<TokenResultDto>> RefreshToken(string authenticationToken, string refreshToken)\r\n        {\r\n            var principal = _tokenService.GetPrincipalFromExpiredToken(authenticationToken);\r\n            var username = principal.Identity.Name;\r\n\r\n            var user = await _userManager.FindByNameAsync(username);\r\n            if (user == null || user.RefreshToken != refreshToken) return BadRequest();\r\n\r\n            var newJwtToken = _tokenService.GenerateAccessToken(username, principal.Claims);\r\n            var newRefreshToken = _tokenService.GenerateRefreshToken();\r\n\r\n            user.RefreshToken = newRefreshToken.Token;\r\n            user.RefreshTokenExpired = newRefreshToken.Expiry;\r\n            await _userManager.UpdateAsync(user);\r\n\r\n            return Ok(new TokenResultDto\r\n            {\r\n                AuthenticationToken = newJwtToken,\r\n                RefreshToken = newRefreshToken.Token\r\n            });\r\n        }\r\n\r\n        [HttpPost]\r\n        [AllowAnonymous]\r\n        public async Task<IActionResult> ConfirmEmail(ConfirmEmailDto input)\r\n        {\r\n            if (string.IsNullOrWhiteSpace(input.UserId))\r\n            {\r\n                ModelState.AddModelError<ConfirmEmailDto>(x => x.UserId, \"Mandatory\");\r\n            }\r\n\r\n            if (string.IsNullOrWhiteSpace(input.Code))\r\n            {\r\n                ModelState.AddModelError<ConfirmEmailDto>(x => x.Code, \"Mandatory\");\r\n            }\r\n\r\n            if (!ModelState.IsValid)\r\n            {\r\n                return BadRequest(ModelState);\r\n            }\r\n\r\n            var userId = input.UserId!;\r\n            var code = input.Code!;\r\n            var user = await _userManager.FindByIdAsync(input.UserId!);\r\n            if (user == null)\r\n            {\r\n                return NotFound($\"Unable to load user with ID '{userId}'.\");\r\n            }\r\n\r\n            code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));\r\n\r\n            var result = await _userManager.ConfirmEmailAsync(user, code);\r\n            if (!result.Succeeded)\r\n            {\r\n                ModelState.AddModelError<ConfirmEmailDto>(x => x, \"Error confirming your email.\");\r\n                return BadRequest(ModelState);\r\n            }\r\n\r\n            return Ok();\r\n        }\r\n\r\n        [HttpPost]\r\n        [Authorize]\r\n        public async Task<IActionResult> Logout()\r\n        {\r\n            var username = User.Identity?.Name;\r\n            var user = await _userManager.FindByNameAsync(username);\r\n            user.RefreshToken = null;\r\n            user.RefreshTokenExpired = null;\r\n            await _userManager.UpdateAsync(user);\r\n            \r\n            _logger.LogInformation($\"User [{username}] logged out the system.\");\r\n            return Ok();\r\n        }\r\n    }\r\n\r\n    public class TokenResultDto\r\n    {\r\n        public string? AuthenticationToken { get; set; }\r\n        public string? RefreshToken { get; set; }\r\n    }\r\n\r\n    public class RegisterDto\r\n    {\r\n        public string? Email { get; set; }\r\n        public string? Password { get; set; }\r\n    }\r\n\r\n    public class LoginDto\r\n    {\r\n        public string? Email { get; set; }\r\n        public string? Password { get; set; }\r\n    }\r\n\r\n    public class ConfirmEmailDto\r\n    {\r\n        public string? UserId { get; set; }\r\n        public string? Code { get; set; }\r\n    }\r\n}\r\n");
            return this.GenerationEnvironment.ToString();
        }
    }
    
}
