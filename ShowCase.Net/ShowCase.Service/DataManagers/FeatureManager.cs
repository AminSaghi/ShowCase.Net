using Microsoft.EntityFrameworkCore;
using ShowCase.Data.Contracts.OperationResults;
using ShowCase.Data.DbContexts;
using ShowCase.Data.Models.ApiModels.Feature;
using ShowCase.Data.Models.Entities;
using ShowCase.Util.StaticClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowCase.Service.DataManagers
{
    public class FeatureManager
    {
        //#region Singleton

        //private static readonly FeatureManager instance = new FeatureManager(DataDbContext.CreateContext());

        //static FeatureManager()
        //{
        //}

        //private FeatureManager(DataDbContext dbcontext)
        //{
        //    db = dbcontext;
        //}

        //public static FeatureManager Instance => instance;

        //#endregion

        public FeatureManager(DataDbContext dbcontext, ProjectManager projectManager)
        {
            db = dbcontext;
            ProjectManager = projectManager;
        }

        private DataDbContext db;
        private ProjectManager ProjectManager { get; }

        #region CRUD

        public async Task<CrudOperationResult<List<Feature>>> GetFeaturesAsync(bool onlyPublished = true)
        {
            try
            {
                var features = await db.Features
                    .Where(p => p.Published == onlyPublished)
                    .OrderBy(p => p.OrderIndex)
                    .ThenBy(p => p.UpdateDateTime)
                    .ToListAsync();

                return new CrudOperationResult<List<Feature>>
                {
                    Success = true,
                    StatusCode = 200,
                    ReturningValue = features
                };
            }
            catch (Exception ex)
            {
                return new CrudOperationResult<List<Feature>>
                {
                    Success = false,
                    StatusCode = 500,
                    Message = ex.Message
                };
            }
        }

        public async Task<CrudOperationResult<Feature>> GetFeatureAsync(int id, bool onlyPublished = true)
        {
            try
            {
                var feature = await db.Features
                     .FirstOrDefaultAsync(p => p.Id == id && p.Published == onlyPublished);

                return new CrudOperationResult<Feature>
                {
                    Success = true,
                    StatusCode = 200,
                    ReturningValue = feature
                };
            }
            catch (Exception ex)
            {
                return new CrudOperationResult<Feature>
                {
                    Success = false,
                    StatusCode = 500,
                    Message = ex.Message
                };
            }
        }

        public async Task<CrudOperationResult<Feature>> CreateFeatureAsync(CreateFeatureApiModel model)
        {
            try
            {
                Project project = null;
                var getProjectResult = await ProjectManager.GetProjectAsync(model.projectId, false);
                if (getProjectResult.Success)
                {
                    project = getProjectResult.ReturningValue;
                }
                else
                {
                    return new CrudOperationResult<Feature>
                    {
                        Success = false,
                        StatusCode = getProjectResult.StatusCode,
                        Message = getProjectResult.Message
                    };
                }

                Feature parent = null;
                if (model.parentId > 0)
                {
                    var getParentResult = await GetFeatureAsync(model.parentId, false);
                    if (getParentResult.Success)
                    {
                        parent = getParentResult.ReturningValue;
                    }
                    else
                    {
                        return new CrudOperationResult<Feature>
                        {
                            Success = false,
                            StatusCode = 400,
                            Message = ReturningMessages.InvalidDataSupplied
                        };
                    }
                }

                var orderIndex = await db.Features
                    .Select(f => f.OrderIndex)
                    .DefaultIfEmpty(0)
                    .MaxAsync();

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

                return new CrudOperationResult<Feature>
                {
                    Success = true,
                    StatusCode = 200,
                    Message = ReturningMessages.CreateSuccessful(feature),
                    ReturningValue = feature
                };
            }
            catch (Exception ex)
            {
                return new CrudOperationResult<Feature>
                {
                    Success = false,
                    StatusCode = 500,
                    Message = ex.Message
                };
            }
        }

        public async Task<CrudOperationResult<Feature>> UpdateFeatureAsync(EditFeatureApiModel model)
        {
            try
            {
                var getFeatureResult = await GetFeatureAsync(model.id, false);            
                if (getFeatureResult.Success)
                {
                    var feature = getFeatureResult.ReturningValue;

                    var getProjectResult = await ProjectManager.GetProjectAsync(model.projectId);
                    if (getProjectResult.Success)
                    {
                        feature.Project = getProjectResult.ReturningValue;
                    }
                    else
                    {
                        return new CrudOperationResult<Feature>
                        {
                            Success = false,
                            StatusCode = getProjectResult.StatusCode,
                            Message = getProjectResult.Message
                        };
                    }

                    if (model.parentId > 0)
                    {
                        var getParentResult = await GetFeatureAsync(model.parentId, false);
                        if (getParentResult.Success)
                        {
                            feature.Parent = getParentResult.ReturningValue;
                        }
                        else
                        {
                            return new CrudOperationResult<Feature>
                            {
                                Success = false,
                                StatusCode = 400,
                                Message = ReturningMessages.InvalidDataSupplied
                            };                          
                        }
                    }
                    else
                    {
                        feature.Parent = null;
                    }

                    feature.OrderIndex = model.orderIndex;
                    feature.Title = model.title;
                    feature.Slug = model.slug;
                    feature.Content = model.content;
                    feature.UpdateDateTime = DateTimeOffset.Now;
                    feature.Published = model.published;

                    db.Entry(getFeatureResult).State = EntityState.Modified;
                    await db.SaveChangesAsync();

                    return new CrudOperationResult<Feature>
                    {
                        Success = true,
                        StatusCode = 200,
                        ReturningValue = feature,
                        Message = ReturningMessages.UpdateSuccessful(feature)
                    };                    
                }
                else
                {
                    return new CrudOperationResult<Feature>
                    {
                        Success = false,
                        StatusCode = 404,
                        Message = ReturningMessages.NotFound(getFeatureResult.ReturningValue)
                    };                 
                }
            }
            catch (Exception ex)
            {
                return new CrudOperationResult<Feature>
                {
                    Success = false,
                    StatusCode = 500,
                    Message = ex.Message
                };                
            }
        }

        public async Task<CrudOperationResult<Feature>> DeleteFeatureAsync(int id)
        {
            try
            {
                var getFeatureResult = await GetFeatureAsync(id, false);
                if (getFeatureResult.Success)
                {
                    var feature = getFeatureResult.ReturningValue;

                    db.Features.Remove(feature);
                    await db.SaveChangesAsync();

                    return new CrudOperationResult<Feature>
                    {
                        Success = true,
                        StatusCode = 200,
                        Message = ReturningMessages.DeleteSuccessful(feature)
                    };                    
                }
                else
                {
                    return new CrudOperationResult<Feature>
                    {
                        Success = false,
                        StatusCode = 404,
                        Message = ReturningMessages.NotFound(getFeatureResult.ReturningValue)
                    };                    
                }
            }
            catch (Exception ex)
            {
                return new CrudOperationResult<Feature>
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
