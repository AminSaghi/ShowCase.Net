using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

using ShowCase.Data.Models.ApiModels.Page;
using ShowCase.Service.DataManagers;
using ShowCase.Util.StaticClasses;

namespace ShowCase.Api.Controllers
{
    public class PagesController : BaseController
    {        
        #region CRUD

        [HttpGet]
        public async Task<IActionResult> GetPublishedPages()
        {
            var getPagesResult = await PageManager.Instance.GetPagesAsync();
            if (getPagesResult.Success)
            {
                return Ok(getPagesResult.ReturningValue.ToArray().Adapt<ListPagesApiModel[]>());
            }
            else
            {
                return StatusCode(500, getPagesResult.Message);
            }            
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPublishedPage(int id)
        {
            var getPageResult = await PageManager.Instance.GetPageAsync(id);
            if (getPageResult.Success)
            {
                return Ok(getPageResult.ReturningValue.Adapt<PageApiModel>());
            }
            else
            {
                return StatusCode(500, getPageResult.Message);
            }            
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> PostPage(CreatePageApiModel model)
        {
            if (ModelState.IsValid)
            {
                var createPageResult = await PageManager.Instance.CreatePageAsync(model);
                if (createPageResult.Success)
                {
                    return Ok(createPageResult.Message);
                }
                else
                {
                    return StatusCode(createPageResult.StatusCode, createPageResult.Message);
                }                
            }
            else
            {
                return BadRequest(ReturningMessages.InvalidDataSupplied());
            }
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> PutPage(EditPageApiModel model)
        {
            if (ModelState.IsValid)
            {
                var updatePageResult = await PageManager.Instance.UpdatePageAsync(model);
                if (updatePageResult.Success)
                {
                    return Ok(updatePageResult.Message);
                }
                else
                {
                    return StatusCode(updatePageResult.StatusCode, updatePageResult.Message);
                }
            }
            else
            {
                return BadRequest(ReturningMessages.InvalidDataSupplied());
            }
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePage(int id)
        {
            var deletePageResult = await PageManager.Instance.DeletePageAsync(id);
            if (deletePageResult.Success)
            {
                return Ok(deletePageResult.Message);
            }
            else
            {
                return StatusCode(deletePageResult.StatusCode, deletePageResult.Message);
            }
        }

        #endregion
    }
}
