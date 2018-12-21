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
        public PagesController(PageManager pageManager)
        {
            PageManager = pageManager;
        }

        private PageManager PageManager { get; }

        #region CRUD

        [HttpGet]
        public async Task<IActionResult> GetPages()
        {
            var onlyPublished = !User.Identity.IsAuthenticated;

            var getPagesResult = await PageManager.GetPagesAsync(onlyPublished);
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
        public async Task<IActionResult> GetPage(int id)
        {
            var onlyPublished = !User.Identity.IsAuthenticated;

            var getPageResult = await PageManager.GetPageAsync(id, onlyPublished);
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
                var createPageResult = await PageManager.CreatePageAsync(model);
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
                var updatePageResult = await PageManager.UpdatePageAsync(model);
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
            var deletePageResult = await PageManager.DeletePageAsync(id);
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
