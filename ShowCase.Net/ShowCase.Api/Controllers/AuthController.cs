using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ShowCase.Data.Models.ApiModels.Account;
using ShowCase.Data.Models.ApiModels.User;
using ShowCase.Util.StaticClasses;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ShowCase.Api.Controllers
{
    [Authorize]
    public class AuthController : BaseController
    {
        public AuthController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            UserManager = userManager;
        }

        public UserManager<IdentityUser> UserManager { get; private set; }
        public SignInManager<IdentityUser> SignInManager { get; private set; }

        public async Task<IActionResult> GetUsers()
        {
            var users = await UserManager.Users.ToArrayAsync();

            return Ok(users.Adapt<ListUsersApiModel[]>());
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> PostLogin([FromBody] LoginApiModel model)
        {
            if (ModelState.IsValid)
            {
                var validCredentials = await CheckCredentials(model.UserName, model.Password);
                if (validCredentials)
                {
                    var now = DateTime.Now;

                    var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecuritySettings.JwtSecret));
                    var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
                    var userClaims = new Claim[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, model.UserName.ToLower())                      
                    };
                    var jwtToken = new JwtSecurityToken(            
                        claims: userClaims, 
                        notBefore: now,
                        expires: now.AddMinutes(SecuritySettings.JwtTokenExpireMins),
                        signingCredentials: signingCredentials);
                    var jwtTokenHandler = new JwtSecurityTokenHandler().WriteToken(jwtToken);

                    return Ok(new { token = jwtTokenHandler });
                }
                else
                {
                    return BadRequest(ReturningMessages.InvalidUserNameOrPassword);
                }
            }
            else
            {
                return BadRequest(ReturningMessages.ModelStateErrors(ModelState));
            }
        }

        [HttpPost("changePassword")]
        public async Task<IActionResult> PostChangePassword([FromBody] ChangePasswordApiModel model)
        {
            if (ModelState.IsValid)
            {
                var userName = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var identityUser = await UserManager.FindByNameAsync(userName);

                var changePasswordResult = await UserManager.ChangePasswordAsync(identityUser, model.currentPassword, model.newPassword);
                if (changePasswordResult.Succeeded)
                {
                    return Ok(ReturningMessages.PasswordChangedSuccessfully);
                }
                else
                {                    
                    return BadRequest(ReturningMessages.IdentityResultErrors(changePasswordResult));
                }                
            }
            else
            {
                return BadRequest(ReturningMessages.ModelStateErrors(ModelState));
            }
        }

        private async Task<bool> CheckCredentials(string userName, string password)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
            {
                return await Task.FromResult(false);
            }

            /* 
             * get UserIdentity to verifty. 
             */
            var identityUser = await UserManager.FindByNameAsync(userName);
            if (identityUser == null)
            {
                return await Task.FromResult(false);
            }

            /* 
             * check the credentials. 
             */
            if (await UserManager.CheckPasswordAsync(identityUser, password))
            {
                return await Task.FromResult(true);
            }
            else
            {
                return await Task.FromResult(false);
            }
        }
    }
}
