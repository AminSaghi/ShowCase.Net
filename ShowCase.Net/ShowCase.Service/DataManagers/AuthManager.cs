using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ShowCase.Data.Contracts.OperationResults;
using ShowCase.Data.DbContexts;
using ShowCase.Data.Models.ApiModels.Account;
using ShowCase.Data.Models.ApiModels.Project;
using ShowCase.Data.Models.ApiModels.User;
using ShowCase.Data.Models.Entities;
using ShowCase.Util.StaticClasses;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ShowCase.Service.DataManagers
{
    public class AuthManager
    {
        public AuthManager(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            UserManager = userManager;
        }

        public UserManager<IdentityUser> UserManager { get; private set; }
        public SignInManager<IdentityUser> SignInManager { get; private set; }

        #region Auth Methods

        public async Task<AuthOperationResult<string>> Login(LoginApiModel model)
        {
            try
            {
                var identityUser = await CheckCredentials(model.UserName, model.Password);
                if (identityUser != null)
                {
                    var now = DateTime.Now;

                    var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecuritySettings.JwtSecret));
                    var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
                    var userClaims = new Claim[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, identityUser.Id)
                    };
                    var jwtToken = new JwtSecurityToken(
                        claims: userClaims,
                        notBefore: now,
                        expires: now.AddMinutes(SecuritySettings.JwtTokenExpireMins),
                        signingCredentials: signingCredentials);
                    var jwtTokenHandler = new JwtSecurityTokenHandler().WriteToken(jwtToken);

                    return new AuthOperationResult<string>
                    {
                        Success = true,
                        Message = jwtTokenHandler
                    };                    
                }
                else
                {
                    return new AuthOperationResult<string>
                    {
                        Success = false,
                        StatusCode = 401,
                        Message = ReturningMessages.InvalidUserNameOrPassword
                    };
                }
            }
            catch (Exception ex)
            {
                return new AuthOperationResult<string>
                {
                    Success = false,
                    StatusCode = 500,
                    Message = ex.Message
                };
            }
        }

        public async Task<AuthOperationResult<object>> PostChangePassword(ChangePasswordApiModel model, ClaimsPrincipal user)
        {
            try
            {
                var identityUser = await GetCurrentIdentityUser(user);

                var changePasswordResult = await UserManager.ChangePasswordAsync(identityUser, model.currentPassword, model.newPassword);
                if (changePasswordResult.Succeeded)
                {
                    return new AuthOperationResult<object>
                    {
                        Success = true,
                        StatusCode = 200,
                        Message = ReturningMessages.PasswordChangedSuccessfully
                    };                  
                }
                else
                {
                    return new AuthOperationResult<object>
                    {
                        Success = false,
                        StatusCode = 400,
                        Message = ReturningMessages.IdentityResultErrors(changePasswordResult)
                    };
                }
            }
            catch (Exception ex)
            {
                return new AuthOperationResult<object>
                {
                    Success = false,
                    StatusCode = 500,
                    Message = ex.Message,
                };
            }
        }

        #endregion

        #region CRUD

        public async Task<CrudOperationResult<List<IdentityUser>>> GetIdentityUsersAsync()
        {
            try
            {
                var users = await UserManager.Users.ToListAsync();

                return new CrudOperationResult<List<IdentityUser>>
                {
                    Success = true,
                    StatusCode = 200,
                    ReturningValue = users
                };
            }
            catch (Exception ex)
            {
                return new CrudOperationResult<List<IdentityUser>>
                {
                    Success = false,
                    StatusCode = 500,
                    Message = ex.Message
                };
            }
        }

        public async Task<CrudOperationResult<IdentityUser>> GetIdentityUserAsync(string userId)
        {
            try
            {
                var identityUser = await GetIdentityUserById(userId);
                if (identityUser != null)
                {
                    return new CrudOperationResult<IdentityUser>
                    {
                        Success = true,
                        StatusCode = 200,
                        ReturningValue = identityUser
                    };
                }
                else
                {
                    return new CrudOperationResult<IdentityUser>
                    {
                        Success = false,
                        StatusCode = 404,
                        ReturningValue = null
                    };
                }
            }
            catch (Exception ex)
            {
                return new CrudOperationResult<IdentityUser>
                {
                    Success = false,
                    StatusCode = 500,
                    Message = ex.Message
                };
            }
        }

        public async Task<CrudOperationResult<IdentityUser>> CreateIdentityUserAsync(CreateUserApiModel model)
        {
            try
            {
                var identityUser = await GetIdentityUserByUserName(model.userName);
                if (identityUser == null)
                {
                    var user = new IdentityUser
                    {
                        UserName = model.userName,
                        Email = model.email
                    };

                    var createUserResult = await UserManager.CreateAsync(user, model.password);
                    if (createUserResult.Succeeded)
                    {
                        var addToRoleResult = await UserManager.AddToRoleAsync(user, "Admin");
                        if (addToRoleResult.Succeeded)
                        {
                            return new CrudOperationResult<IdentityUser>
                            {
                                Success = true,
                                StatusCode = 200,
                                Message = ReturningMessages.CreateSuccessful(user)
                            };
                        }
                        else
                        {
                            await DeleteIdentityUserAsync(user.Id);

                            return new CrudOperationResult<IdentityUser>
                            {
                                Success = false,
                                StatusCode = 500,
                                Message = ReturningMessages.IdentityResultErrors(addToRoleResult)
                            };
                        }
                    }
                    else
                    {
                        return new CrudOperationResult<IdentityUser>
                        {
                            Success = false,
                            StatusCode = 400,
                            Message = ReturningMessages.IdentityResultErrors(createUserResult)
                        };
                    }
                }
                else
                {
                    return new CrudOperationResult<IdentityUser>
                    {
                        Success = false,
                        StatusCode = 400,
                        Message = ReturningMessages.UserExists,                        
                        ReturningValue = identityUser
                    };
                }
            }
            catch (Exception ex)
            {
                return new CrudOperationResult<IdentityUser>
                {
                    Success = false,
                    StatusCode = 500,
                    Message = ex.Message
                };
            }
        }

        public async Task<CrudOperationResult<IdentityUser>> UpdateIdentityUserAsync(EditUserApiModel model)
        {
            try
            {
                var getUserResult = await GetIdentityUserAsync(model.id);            
                if (getUserResult.Success)
                {
                    var user = getUserResult.ReturningValue;                    

                    user.UserName = model.userName;
                    user.Email = model.email;
                    
                    var updateResult = await UserManager.UpdateAsync(user);
                    if (updateResult.Succeeded)
                    {
                        return new CrudOperationResult<IdentityUser>
                        {
                            Success = true,
                            StatusCode = 200,
                            ReturningValue = user,
                            Message = ReturningMessages.UpdateSuccessful(user)
                        };
                    }   
                    else
                    {
                        return new CrudOperationResult<IdentityUser>
                        {
                            Success = false,
                            StatusCode = 400,
                            Message = ReturningMessages.IdentityResultErrors(updateResult)
                        };
                    }
                }
                else
                {
                    return new CrudOperationResult<IdentityUser>
                    {
                        Success = false,
                        StatusCode = 404,
                        Message = ReturningMessages.NotFound(getUserResult.ReturningValue)
                    };                 
                }
            }
            catch (Exception ex)
            {
                return new CrudOperationResult<IdentityUser>
                {
                    Success = false,
                    StatusCode = 500,
                    Message = ex.Message
                };                
            }
        }

        public async Task<CrudOperationResult<IdentityUser>> DeleteIdentityUserAsync(string userId)
        {
            try
            {
                var identityUser = await GetIdentityUserById(userId);
                if (identityUser != null)
                {
                    var deleteResult = await UserManager.DeleteAsync(identityUser);
                    if (deleteResult.Succeeded)
                    {
                        return new CrudOperationResult<IdentityUser>
                        {
                            Success = true,
                            StatusCode = 200,
                            Message = ReturningMessages.DeleteSuccessful(identityUser)
                        };
                    }    
                    else
                    {
                        return new CrudOperationResult<IdentityUser>
                        {
                            Success = true,
                            StatusCode = 500,
                            Message = ReturningMessages.IdentityResultErrors(deleteResult)
                        };
                    }
                }
                else
                {
                    return new CrudOperationResult<IdentityUser>
                    {
                        Success = false,
                        StatusCode = 404,
                        Message = ReturningMessages.NotFound(identityUser)
                    };                    
                }
            }
            catch (Exception ex)
            {
                return new CrudOperationResult<IdentityUser>
                {
                    Success = false,
                    StatusCode = 500,
                    Message = ex.Message
                };
            }
        }

        #endregion

        #region Private Methods

        private async Task<IdentityUser> CheckCredentials(string userName, string password)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
            {
                return null;
            }

            /* 
             * get UserIdentity to verifty 
             * and check its password. 
             */
            var identityUser = await GetIdentityUserByUserName(userName);
            if (identityUser != null)
            {
                var checkPasswordResult = await UserManager.CheckPasswordAsync(identityUser, password);
                if (!checkPasswordResult)
                {
                    identityUser = null;
                }
            }

            return identityUser;
        }

        private async Task<IdentityUser> GetCurrentIdentityUser(ClaimsPrincipal user)
        {
            var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!string.IsNullOrEmpty(userId))
            {
                return await UserManager.FindByIdAsync(userId);
            }

            return null;
        }

        private async Task<IdentityUser> GetIdentityUserById(string userId)
        {
            if (!string.IsNullOrEmpty(userId))
            {
                return await UserManager.FindByIdAsync(userId);
            }

            return null;
        }

        private async Task<IdentityUser> GetIdentityUserByUserName(string userName)
        {
            if (!string.IsNullOrEmpty(userName))
            {
                return await UserManager.FindByNameAsync(userName);
            }

            return null;
        }

        #endregion
    }
}
