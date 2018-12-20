using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

using ShowCase.Data.DbContexts;
using ShowCase.Data.Models.ApiModels.Feature;
using ShowCase.Data.Models.Entities;

namespace ShowCase.Api.Controllers
{
    public class FeaturesController : BaseController
    {
        DataDbContext db;

        public FeaturesController(DataDbContext context)
        {            
            db = context;
        }

        #region CRUD

        [HttpGet]
        public async Task<IActionResult> GetPublishedFeatures()
        {
            try
            {
                var features = await db.Features
                    .Where(p => p.Published)
                    .OrderBy(p => p.OrderIndex)
                    .ThenBy(p => p.UpdateDateTime)
                    .ToListAsync();
                
                return Ok(features.ToArray().Adapt<ListFeaturesApiModel[]>());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPublishedFeature(int id)
        {
            try
            {
                var feature = await db.Features
                    .FirstOrDefaultAsync(p => p.Id == id && p.Published);

                if (feature != null)
                {                    
                    return Ok(feature.Adapt<FeatureApiModel>());
                }
                else
                {
                    //TODO:
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
        public async Task<IActionResult> PostFeature(CreateFeatureApiModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {                    
                    var project = await db.Projects
                        .FirstOrDefaultAsync(p => p.Id == model.projectId);

                    if (project == null)
                    {
                        //TODO: get message from the helper.
                        return BadRequest("");
                    }

                    Feature parent = null;
                    if (model.parentId > 0)
                    {
                        parent = await db.Features
                            .FirstOrDefaultAsync(p => p.Id == model.parentId);

                        if (parent == null)
                        {
                            //TODO: get message from the helper.
                            return BadRequest("");
                        }
                    }

                    var orderIndex = await db.Features.MaxAsync(p => p.OrderIndex);

                    var now = DateTimeOffset.Now;

                    var feature = new Feature
                    {
                        Project = project,
                        Parent = parent,

                        OrderIndex = ++orderIndex,
                        Title = model.title,
                        Slug = model.slug,
                        Content = model.content,

                        CreateDateTime = now,
                        UpdateDateTime = now,

                        Published = false
                    };

                    db.Features.Add(feature);
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
        public async Task<IActionResult> PutFeature(EditFeatureApiModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var feature = await db.Features
                        .Include(p => p.Parent)
                        .FirstOrDefaultAsync(p => p.Id == model.id);

                    if (feature != null)
                    {
                        var project = await db.Projects
                            .FirstOrDefaultAsync(p => p.Id == model.projectId);

                        if (project != null)
                        {
                            feature.Project = project;
                        }
                        else
                        {
                            //TODO: get message from the helper.
                            return BadRequest("");
                        }

                        if (model.parentId > 0)
                        {
                            var parent = await db.Features
                                .FirstOrDefaultAsync(p => p.Id == model.parentId);

                            if (parent != null)
                            {
                                feature.Parent = parent;                                
                            }
                            else
                            {
                                //TODO: get message from the helper.
                                return BadRequest("");
                            }
                        }

                        feature.OrderIndex = model.orderIndex;
                        feature.Title = model.title;
                        feature.Slug = model.slug;
                        feature.Content = model.content;
                        feature.UpdateDateTime = DateTimeOffset.Now;
                        feature.Published = model.published;

                        db.Entry(feature).State = EntityState.Modified;
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
        public async Task<IActionResult> DeleteFeature(int id)
        {
            try
            {
                var feature = await db.Features
                    .FirstOrDefaultAsync(p => p.Id == id);

                if (feature != null)
                {
                    db.Features.Remove(feature);
                    await db.SaveChangesAsync();

                    return Ok("");
                }
                else
                {
                    //TODO:
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
