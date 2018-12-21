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
        #region CRUD

        [HttpGet]
        public async Task<IActionResult> GetPublishedFeatures()
        {
            var getFeaturesResult = await FeatureManager.Instance.GetFeaturesAsync();
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
        public async Task<IActionResult> GetPublishedFeature(int id)
        {
            var getFeatureResult = await FeatureManager.Instance.GetFeatureAsync(id);
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
                var createFeatureResult = await FeatureManager.Instance.CreateFeatureAsync(model);
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
                var updateFeatureResult = await FeatureManager.Instance.UpdateFeatureAsync(model);
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
            var deleteFeatureResult = await FeatureManager.Instance.DeleteFeatureAsync(id);
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
