using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShowCase.Data.DbContexts;
using ShowCase.Data.Models.ApiModels.Page;
using ShowCase.Data.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShowCase.Api.Controllers
{
    public class PagesController : BaseController
    {
        DataDbContext db;

        public PagesController(DataDbContext context)
        {            
            db = context;
        }

        #region CRUD

        [HttpGet]
        public async Task<IActionResult> GetPublishedPages()
        {
            try
            {
                var pages = await db.Pages
                    .Where(p => p.Published)
                    .OrderBy(p => p.OrderIndex)
                    .ThenBy(p => p.UpdateDateTime)
                    .ToListAsync();
                
                return Ok(pages.ToArray().Adapt<ListPagesApiModel[]>());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPublishedPage(int id)
        {
            try
            {
                var page = await db.Pages
                    .FirstOrDefaultAsync(p => p.Id == id && p.Published);

                if (page != null)
                {                    
                    return Ok(page.Adapt<PageApiModel>());
                }
                else
                {
                    return NotFound("Page NOT found.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> PostPage(CreatePageApiModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Page parent = null;
                    if (model.parentId > 0)
                    {
                        parent = await db.Pages
                            .FirstOrDefaultAsync(p => p.Id == model.parentId);

                        if (parent == null)
                        {
                            //TODO: get message from the helper.
                            return BadRequest("");
                        }
                    }

                    var orderIndex = await db.Pages.MaxAsync(p => p.OrderIndex);

                    var now = DateTimeOffset.Now;

                    var page = new Page
                    {
                        Parent = parent,

                        OrderIndex = ++orderIndex,
                        Title = model.title,
                        Slug = model.slug,
                        Content = model.content,

                        CreateDateTime = now,
                        UpdateDateTime = now,

                        Published = false
                    };

                    db.Pages.Add(page);
                    await db.SaveChangesAsync();

                    //TODO: create messages helper.
                    return Ok("");
                }
                catch (Exception ex)
                {
                    return StatusCode(500, ex.Message);
                }
            }
            else
            {
                return BadRequest("Invalid data supplied.");
            }
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> PutPage(EditPageApiModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var page = await db.Pages
                        .Include(p => p.Parent)
                        .FirstOrDefaultAsync(p => p.Id == model.id);

                    if (page != null)
                    {
                        if (model.parentId > 0)
                        {
                            var parent = await db.Pages
                                .FirstOrDefaultAsync(p => p.Id == model.parentId);

                            if (parent != null)
                            {
                                page.Parent = parent;                                
                            }
                            else
                            {
                                //TODO: get message from the helper.
                                return BadRequest("");
                            }
                        }
                        page.OrderIndex = model.orderIndex;
                        page.Title = model.title;
                        page.Slug = model.slug;
                        page.Content = model.content;
                        page.UpdateDateTime = DateTimeOffset.Now;
                        page.Published = model.published;

                        db.Entry(page).State = EntityState.Modified;
                        await db.SaveChangesAsync();
                        //TODO: message.
                        return Ok("");
                    }
                    else
                    {
                        return NotFound("");
                    }
                }
                catch (Exception ex)
                {
                    return StatusCode(500, ex.Message);
                }
            }
            else
            {
                return BadRequest("Invalid data supplied.");
            }
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePage(int id)
        {
            try
            {
                var page = await db.Pages
                    .FirstOrDefaultAsync(p => p.Id == id);

                if (page != null)
                {
                    db.Pages.Remove(page);
                    await db.SaveChangesAsync();

                    return Ok("");
                }
                else
                {
                    return NotFound("Page NOT found.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        #endregion
    }
}
