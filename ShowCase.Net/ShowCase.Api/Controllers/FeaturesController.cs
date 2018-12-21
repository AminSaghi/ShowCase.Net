using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

using ShowCase.Data.Models.ApiModels.Feature;
using ShowCase.Util.StaticClasses;
using ShowCase.Service.DataManagers;

namespace ShowCase.Api.Controllers
{
    public class FeaturesController : BaseController
    {
        public FeaturesController(FeatureManager featureManager)
        {
            FeatureManager = featureManager;
        }

        private FeatureManager FeatureManager { get; }

        #region CRUD

        [HttpGet]
        public async Task<IActionResult> GetFeatures()
        {
            var onlyPublished = !User.Identity.IsAuthenticated;

            var getFeaturesResult = await FeatureManager.GetFeaturesAsync(onlyPublished);
            if (getFeaturesResult.Success)
            {
                return Ok(getFeaturesResult.ReturningValue.ToArray().Adapt<ListFeaturesApiModel[]>());
            }
            else
            {
                return StatusCode(500, getFeaturesResult.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFeature(int id)
        {
            var onlyPublished = !User.Identity.IsAuthenticated;

            var getFeatureResult = await FeatureManager.GetFeatureAsync(id, onlyPublished);
            if (getFeatureResult.Success)
            {
                return Ok(getFeatureResult.ReturningValue.Adapt<FeatureApiModel>());
            }
            else
            {
                return StatusCode(500, getFeatureResult.Message);
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> PostFeature(CreateFeatureApiModel model)
        {            
            if (ModelState.IsValid)
            {
                var createFeatureResult = await FeatureManager.CreateFeatureAsync(model);
                if (createFeatureResult.Success)
                {
                    return Ok(createFeatureResult.Message);
                }
                else
                {
                    return StatusCode(createFeatureResult.StatusCode, createFeatureResult.Message);
                }
            }
            else
            {
                return BadRequest(ReturningMessages.InvalidDataSupplied());
            }
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> PutFeature(EditFeatureApiModel model)
        {            
            if (ModelState.IsValid)
            {
                var updateFeatureResult = await FeatureManager.UpdateFeatureAsync(model);
                if (updateFeatureResult.Success)
                {
                    return Ok(updateFeatureResult.Message);
                }
                else
                {
                    return StatusCode(updateFeatureResult.StatusCode, updateFeatureResult.Message);
                }
            }
            else
            {
                return BadRequest(ReturningMessages.InvalidDataSupplied());
            }
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFeature(int id)
        {
            var deleteFeatureResult = await FeatureManager.DeleteFeatureAsync(id);
            if (deleteFeatureResult.Success)
            {
                return Ok(deleteFeatureResult.Message);
            }
            else
            {
                return StatusCode(deleteFeatureResult.StatusCode, deleteFeatureResult.Message);
            }
        }

        #endregion
    }
}
