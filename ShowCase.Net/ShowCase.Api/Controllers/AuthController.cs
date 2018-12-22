﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ShowCase.Data.Models.ApiModels.Account;
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

        [AllowAnonymous]
        [HttpGet("login")]
        public async Task<IActionResult> GetLogin([FromBody] LoginApiModel model)
        {
            if (ModelState.IsValid)
            {
                var validCredentials = await CheckCredentials(model.UserName, model.Password);
                if (validCredentials)
                {
                    var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecuritySettings.JwtSecret));
                    var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
                    var userClaims = new Claim[]
                    {
                    new Claim(JwtRegisteredClaimNames.UniqueName, model.UserName.ToLower())
                    };
                    var jwtToken = new JwtSecurityToken(claims: userClaims, signingCredentials: signingCredentials);
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
                return BadRequest(ReturningMessages.InvalidDataSupplied());
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
