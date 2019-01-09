using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShowCase.Data.Models.ApiModels.Account;
using ShowCase.Data.Models.ApiModels.User;
using ShowCase.Service.DataManagers;
using ShowCase.Util.StaticClasses;
using System.Threading.Tasks;

namespace ShowCase.Api.Controllers
{
    [Authorize]
    public class AuthController : BaseController
    {
        public AuthController(AuthManager authManager)
        {
            AuthManager = authManager;
        }

        private AuthManager AuthManager { get; }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> PostLogin([FromBody] LoginApiModel model)
        {
            if (ModelState.IsValid)
            {
                var loginResult = await AuthManager.Login(model);
                if (loginResult.Success)
                {
                    return Ok(new { token = loginResult.ReturningValue });
                }
                else
                {
                    return StatusCode(loginResult.StatusCode, loginResult.Message);
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
                var changePasswordResult = await AuthManager.PostChangePassword(model, User);

                return StatusCode(changePasswordResult.StatusCode, changePasswordResult.Message);
            }
            else
            {
                return BadRequest(ReturningMessages.ModelStateErrors(ModelState));
            }
        }

        [HttpGet("users")]
        public async Task<IActionResult> GetUsers()
        {
            var getUsersResult = await AuthManager.GetIdentityUsersAsync();
            if (getUsersResult.Success)
            {
                return Ok(getUsersResult.ReturningValue.ToArray().Adapt<ListUsersApiModel[]>());
            }
            else
            {
                return StatusCode(getUsersResult.StatusCode, getUsersResult.Message);
            }
        }

        [HttpGet("users")]
        public async Task<IActionResult> GetUser(string id)
        {
            var getUserResult = await AuthManager.GetIdentityUserAsync(id);
            if (getUserResult.Success)
            {
                return Ok(getUserResult.ReturningValue.Adapt<ListUsersApiModel>());
            }
            else
            {
                return StatusCode(getUserResult.StatusCode, getUserResult.Message);
            }
        }

        [Authorize]
        [HttpPost("users")]
        public async Task<IActionResult> PostUser([FromBody] CreateUserApiModel model)
        {
            if (ModelState.IsValid)
            {
                var createUserResult = await AuthManager.CreateIdentityUserAsync(model);

                return StatusCode(createUserResult.StatusCode, createUserResult.Message);
            }
            else
            {
                return BadRequest(ReturningMessages.ModelStateErrors(ModelState));
            }
        }

        [Authorize]
        [HttpPut("users")]
        public async Task<IActionResult> PutUser([FromBody] EditUserApiModel model)
        {
            if (ModelState.IsValid)
            {
                var updateUserResult = await AuthManager.UpdateIdentityUserAsync(model);

                return StatusCode(updateUserResult.StatusCode, updateUserResult.Message);
            }
            else
            {
                return BadRequest(ReturningMessages.ModelStateErrors(ModelState));
            }
        }

        [Authorize]
        [HttpDelete("users/{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var deleteUserResult = await AuthManager.DeleteIdentityUserAsync(id);

            return StatusCode(deleteUserResult.StatusCode, deleteUserResult.Message);
        }
    }
}
