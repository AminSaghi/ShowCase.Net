using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

using ShowCase.Data.DbContexts;
using ShowCase.Data.Models.ApiModels.Page;
using ShowCase.Data.Models.ApiModels.Project;
using ShowCase.Data.Models.Entities;
using ShowCase.Util.StaticClasses;

namespace ShowCase.Api.Controllers
{
    public class ProjectsController : BaseController
    {
        DataDbContext db;

        public ProjectsController(DataDbContext context)
        {            
            db = context;
        }

        #region CRUD

        [HttpGet]
        public async Task<IActionResult> GetPublishedProjects()
        {
            try
            {
                var projects = await db.Projects
                    .Where(p => p.Published)
                    .OrderBy(p => p.OrderIndex)
                    .ToListAsync();
                
                return Ok(projects.ToArray().Adapt<ListProjectsApiModel[]>());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPublishedProject(int id)
        {
            try
            {
                var project = await db.Projects
                    .FirstOrDefaultAsync(p => p.Id == id && p.Published);

                if (project != null)
                {                    
                    return Ok(project.Adapt<ProjectApiModel>());
                }
                else
                {
                    return NotFound(ReturningMessages.NotFound(project));
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> PostProject(CreateProjectApiModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var orderIndex = await db.Projects.MaxAsync(p => p.OrderIndex);

                    var now = DateTimeOffset.Now;

                    var project = new Project
                    {                     
                        OrderIndex = ++orderIndex,
                        Title = model.title,
                        Slug = model.slug,
                        ImageUrl = model.imageUrl,

                        Published = false
                    };

                    db.Projects.Add(project);
                    await db.SaveChangesAsync();
                    
                    return Ok(ReturningMessages.CreateSuccessful(project));
                }
                catch (Exception ex)
                {
                    return StatusCode(500, ex.Message);
                }
            }
            else
            {
                return BadRequest(ReturningMessages.InvalidDataSupplied());
            }
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> PutProject(EditProjectApiModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var project = await db.Projects                        
                        .FirstOrDefaultAsync(p => p.Id == model.id);

                    if (project != null)
                    {
                        project.OrderIndex = model.orderIndex;
                        project.Title = model.title;
                        project.Slug = model.slug;

                        project.Published = model.published;

                        db.Entry(project).State = EntityState.Modified;
                        await db.SaveChangesAsync();
                        
                        return Ok(ReturningMessages.UpdateSuccessful(project));
                    }
                    else
                    {
                        return NotFound(ReturningMessages.NotFound(project));
                    }
                }
                catch (Exception ex)
                {
                    return StatusCode(500, ex.Message);
                }
            }
            else
            {
                return BadRequest(ReturningMessages.InvalidDataSupplied());
            }
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(int id)
        {
            try
            {
                var project = await db.Projects
                    .FirstOrDefaultAsync(p => p.Id == id);

                if (project != null)
                {
                    db.Projects.Remove(project);
                    await db.SaveChangesAsync();

                    return Ok(ReturningMessages.DeleteSuccessful(project));
                }
                else
                {
                    return NotFound(ReturningMessages.NotFound(project));
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
