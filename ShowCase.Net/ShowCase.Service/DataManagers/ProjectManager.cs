﻿using Microsoft.EntityFrameworkCore;
using ShowCase.Data.Contracts.OperationResults;
using ShowCase.Data.DbContexts;
using ShowCase.Data.Models.ApiModels.Project;
using ShowCase.Data.Models.Entities;
using ShowCase.Util.StaticClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowCase.Service.DataManagers
{
    public class ProjectManager
    {
        //#region Singleton

        //private static readonly ProjectManager instance = new ProjectManager(DataDbContext.CreateContext());

        //static ProjectManager()
        //{
        //}

        //private ProjectManager(DataDbContext dbcontext)
        //{
        //    db = dbcontext;
        //}

        //public static ProjectManager Instance => instance;

        //#endregion

        public ProjectManager(DataDbContext dbcontext)
        {
            db = dbcontext;
        }

        private DataDbContext db;

        #region CRUD

        public async Task<CrudOperationResult<List<Project>>> GetProjectsAsync(bool onlyPublished = true, bool withImage = false, bool withFeatures = false)
        {
            try
            {
                var query = db.Projects
                    .Where(p => p.Published == onlyPublished);

                if (withFeatures)
                {
                    query.Include(p => p.Features);
                }

                if (withImage)
                {
                    query.Select(p => new Project 
                    { 
                        Id = p.Id,
                        OrderIndex = p.OrderIndex,
                        Title = p.Title,
                        Slug = p.Slug,
                        Description = p.Description,
                        Published = p.Published,
                        ImageUrl = p.ImageUrl,
                        Features = withFeatures ? 
                            p.Features
                                .Where(f => f.Published == onlyPublished)
                                .OrderBy(f => f.OrderIndex)
                            : null
                    });
                }
                else
                {
                    query.Select(p => new Project
                    {
                        Id = p.Id,
                        OrderIndex = p.OrderIndex,
                        Title = p.Title,
                        Slug = p.Slug,
                        Description = p.Description,
                        Published = p.Published,              
                        Features = withFeatures ?
                            p.Features
                                .Where(f => f.Published == onlyPublished)
                                .OrderBy(f => f.OrderIndex)
                            : null
                    });
                }

                var projects = await query
                    .OrderBy(p => p.OrderIndex)           
                    .ToListAsync();

                return new CrudOperationResult<List<Project>>
                {
                    Success = true,
                    StatusCode = 200,
                    ReturningValue = projects
                };
            }
            catch (Exception ex)
            {
                return new CrudOperationResult<List<Project>>
                {
                    Success = false,
                    StatusCode = 500,
                    Message = ex.Message
                };
            }
        }

        public async Task<CrudOperationResult<Project>> GetProjectAsync(int id, bool onlyPublished = true)
        {
            try
            {
                var project = await db.Projects
                     .FirstOrDefaultAsync(p => p.Id == id && p.Published == onlyPublished);

                if (project != null)
                {
                    return new CrudOperationResult<Project>
                    {
                        Success = true,
                        StatusCode = 200,
                        ReturningValue = project
                    };
                }
                else
                {
                    return new CrudOperationResult<Project>
                    {
                        Success = false,
                        StatusCode = 404,
                        ReturningValue = null
                    };
                }
            }
            catch (Exception ex)
            {
                return new CrudOperationResult<Project>
                {
                    Success = false,
                    StatusCode = 500,
                    Message = ex.Message
                };
            }
        }

        public async Task<CrudOperationResult<Project>> CreateProjectAsync(CreateProjectApiModel model)
        {
            try
            {               
                var orderIndex = await db.Projects
                    .Select(p => p.OrderIndex)
                    .DefaultIfEmpty(0)
                    .MaxAsync();

                var now = DateTimeOffset.Now;

                var project = new Project
                {                    
                    OrderIndex = ++orderIndex,
                    Title = model.title,
                    Slug = model.slug,
                    Description = model.description,
                    ImageUrl = model.imageUrl,  

                    Published = false
                };

                db.Projects.Add(project);
                await db.SaveChangesAsync();

                return new CrudOperationResult<Project>
                {
                    Success = true,
                    StatusCode = 200,
                    Message = ReturningMessages.CreateSuccessful(project),
                    ReturningValue = project
                };
            }
            catch (Exception ex)
            {
                return new CrudOperationResult<Project>
                {
                    Success = false,
                    StatusCode = 500,
                    Message = ex.Message
                };
            }
        }

        public async Task<CrudOperationResult<Project>> UpdateProjectAsync(EditProjectApiModel model)
        {
            try
            {
                var getProjectResult = await GetProjectAsync(model.id, false);            
                if (getProjectResult.Success)
                {
                    var project = getProjectResult.ReturningValue;                    

                    project.OrderIndex = model.orderIndex;
                    project.Title = model.title;
                    project.Slug = model.slug;   
                    project.Description = model.description;
                    project.ImageUrl = model.imageUrl;
                    project.Published = model.published;

                    db.Entry(project).State = EntityState.Modified;
                    await db.SaveChangesAsync();

                    return new CrudOperationResult<Project>
                    {
                        Success = true,
                        StatusCode = 200,
                        ReturningValue = project,
                        Message = ReturningMessages.UpdateSuccessful(project)
                    };                    
                }
                else
                {
                    return new CrudOperationResult<Project>
                    {
                        Success = false,
                        StatusCode = 404,
                        Message = ReturningMessages.NotFound(getProjectResult.ReturningValue)
                    };                 
                }
            }
            catch (Exception ex)
            {
                return new CrudOperationResult<Project>
                {
                    Success = false,
                    StatusCode = 500,
                    Message = ex.Message
                };                
            }
        }

        public async Task<CrudOperationResult<Project>> DeleteProjectAsync(int id)
        {
            try
            {
                var getProjectResult = await GetProjectAsync(id, false);
                if (getProjectResult.Success)
                {
                    var project = getProjectResult.ReturningValue;

                    db.Projects.Remove(project);
                    await db.SaveChangesAsync();

                    return new CrudOperationResult<Project>
                    {
                        Success = true,
                        StatusCode = 200,
                        Message = ReturningMessages.DeleteSuccessful(project)
                    };                    
                }
                else
                {
                    return new CrudOperationResult<Project>
                    {
                        Success = false,
                        StatusCode = 404,
                        Message = ReturningMessages.NotFound(getProjectResult.ReturningValue)
                    };                    
                }
            }
            catch (Exception ex)
            {
                return new CrudOperationResult<Project>
                {
                    Success = false,
                    StatusCode = 500,
                    Message = ex.Message
                };
            }
        }

        #endregion

        #region Private Methods



        #endregion
    }
}
