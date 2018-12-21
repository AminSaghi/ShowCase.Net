using Microsoft.EntityFrameworkCore;
using ShowCase.Data.Contracts.OperationResults;
using ShowCase.Data.DbContexts;
using ShowCase.Data.Models.ApiModels.Page;
using ShowCase.Data.Models.Entities;
using ShowCase.Util.StaticClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowCase.Service.DataManagers
{
    public class PageManager
    {
        #region Singleton

        private static readonly PageManager instance = new PageManager(DataDbContext.CreateContext());

        static PageManager()
        {
        }

        private PageManager(DataDbContext dbcontext)
        {
            db = dbcontext;
        }

        public static PageManager Instance => instance;

        #endregion

        private DataDbContext db;

        #region CRUD

        public async Task<CrudOperationResult<List<Page>>> GetPagesAsync(bool onlyPublished = true)
        {
            try
            {
                var pages = await db.Pages
                    .Where(p => p.Published == onlyPublished)
                    .OrderBy(p => p.OrderIndex)
                    .ThenBy(p => p.UpdateDateTime)
                    .ToListAsync();

                return new CrudOperationResult<List<Page>>
                {
                    Success = true,
                    StatusCode = 200,
                    ReturningValue = pages
                };
            }
            catch (Exception ex)
            {
                return new CrudOperationResult<List<Page>>
                {
                    Success = false,
                    StatusCode = 500,
                    Message = ex.Message
                };
            }
        }

        public async Task<CrudOperationResult<Page>> GetPageAsync(int id, bool onlyPublished = true)
        {
            try
            {
                var page = await db.Pages
                     .FirstOrDefaultAsync(p => p.Id == id && p.Published == onlyPublished);

                return new CrudOperationResult<Page>
                {
                    Success = true,
                    StatusCode = 200,
                    ReturningValue = page
                };
            }
            catch (Exception ex)
            {
                return new CrudOperationResult<Page>
                {
                    Success = false,
                    StatusCode = 500,
                    Message = ex.Message
                };
            }
        }

        public async Task<CrudOperationResult<Page>> CreatePageAsync(CreatePageApiModel model)
        {
            try
            {
                Page parent = null;
                if (model.parentId > 0)
                {
                    var getParentResult = await GetPageAsync(model.parentId, false);

                    if (getParentResult.Success)
                    {
                        parent = getParentResult.ReturningValue;
                    }
                    else
                    {
                        return new CrudOperationResult<Page>
                        {
                            Success = false,
                            StatusCode = 400,
                            Message = ReturningMessages.InvalidDataSupplied()
                        };
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

                return new CrudOperationResult<Page>
                {
                    Success = true,
                    StatusCode = 200,
                    Message = ReturningMessages.CreateSuccessful(page),
                    ReturningValue = page
                };
            }
            catch (Exception ex)
            {
                return new CrudOperationResult<Page>
                {
                    Success = false,
                    StatusCode = 500,
                    Message = ex.Message
                };
            }
        }

        public async Task<CrudOperationResult<Page>> UpdatePageAsync(EditPageApiModel model)
        {
            try
            {
                var getPageResult = await GetPageAsync(model.id, false);            
                if (getPageResult.Success)
                {
                    var page = getPageResult.ReturningValue;

                    if (model.parentId > 0)
                    {
                        var getParentResult = await GetPageAsync(model.parentId, false);
                        if (getParentResult.Success)
                        {
                            page.Parent = getParentResult.ReturningValue;
                        }
                        else
                        {
                            return new CrudOperationResult<Page>
                            {
                                Success = false,
                                StatusCode = 400,
                                Message = ReturningMessages.InvalidDataSupplied()
                            };                          
                        }
                    }
                    else
                    {
                        page.Parent = null;
                    }

                    page.OrderIndex = model.orderIndex;
                    page.Title = model.title;
                    page.Slug = model.slug;
                    page.Content = model.content;
                    page.UpdateDateTime = DateTimeOffset.Now;
                    page.Published = model.published;

                    db.Entry(getPageResult).State = EntityState.Modified;
                    await db.SaveChangesAsync();

                    return new CrudOperationResult<Page>
                    {
                        Success = true,
                        StatusCode = 200,
                        ReturningValue = page,
                        Message = ReturningMessages.UpdateSuccessful(page)
                    };                    
                }
                else
                {
                    return new CrudOperationResult<Page>
                    {
                        Success = false,
                        StatusCode = 404,
                        Message = ReturningMessages.NotFound(getPageResult.ReturningValue)
                    };                 
                }
            }
            catch (Exception ex)
            {
                return new CrudOperationResult<Page>
                {
                    Success = false,
                    StatusCode = 500,
                    Message = ex.Message
                };                
            }
        }

        public async Task<CrudOperationResult<Page>> DeletePageAsync(int id)
        {
            try
            {
                var getPageResult = await GetPageAsync(id, false);
                if (getPageResult.Success)
                {
                    var page = getPageResult.ReturningValue;

                    db.Pages.Remove(page);
                    await db.SaveChangesAsync();

                    return new CrudOperationResult<Page>
                    {
                        Success = true,
                        StatusCode = 200,
                        Message = ReturningMessages.DeleteSuccessful(page)
                    };                    
                }
                else
                {
                    return new CrudOperationResult<Page>
                    {
                        Success = false,
                        StatusCode = 404,
                        Message = ReturningMessages.NotFound(getPageResult.ReturningValue)
                    };                    
                }
            }
            catch (Exception ex)
            {
                return new CrudOperationResult<Page>
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
