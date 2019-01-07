using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

using ShowCase.Data.Models.ApiModels.Settings;
using ShowCase.Util.StaticClasses;
using ShowCase.Service.DataManagers;

namespace ShowCase.Api.Controllers
{
    [Authorize]
    public class SettingsController : BaseController
    {
        public SettingsController(SettingsManager settingsManager)
        {
            SettingsManager = settingsManager;
        }

        private SettingsManager SettingsManager { get; }

        #region CRUD

        [HttpGet]
        public async Task<IActionResult> GetSettings()
        {            
            var getSettingsResult = await SettingsManager.GetSettingsAsync();
            if (getSettingsResult.Success)
            {
                return Ok(getSettingsResult.ReturningValue.Adapt<SettingsApiModel>());
            }
            else
            {
                return StatusCode(500, getSettingsResult.Message);
            }
        }               
        
        [HttpPut]
        public async Task<IActionResult> PutSettings([FromBody] SettingsApiModel model)
        {
            if (ModelState.IsValid)
            {
                var updateSettingsResult = await SettingsManager.UpdateSettingsAsync(model);
                if (updateSettingsResult.Success)
                {
                    return Ok(updateSettingsResult.Message);
                }
                else
                {
                    return StatusCode(updateSettingsResult.StatusCode, updateSettingsResult.Message);
                }
            }
            else
            {
                return BadRequest(ReturningMessages.ModelStateErrors(ModelState));
            }
        }        

        #endregion
    }
}
