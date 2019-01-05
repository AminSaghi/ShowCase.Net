using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

using ShowCase.Data.Contracts.OperationResults;
using ShowCase.Data.DbContexts;
using ShowCase.Data.Models.Entities;
using ShowCase.Util.StaticClasses;
using ShowCase.Data.Models.ApiModels.Settings;

namespace ShowCase.Service.DataManagers
{
    public class SettingsManager
    {
        public SettingsManager(DataDbContext dbcontext)
        {
            db = dbcontext;
        }

        private DataDbContext db;

        #region CRUD

        public async Task<CrudOperationResult<Settings>> GetSettingsAsync()
        {
            try
            {
                var settings = await db.Settings.FirstOrDefaultAsync();

                return new CrudOperationResult<Settings>
                {
                    Success = true,
                    StatusCode = 200,
                    ReturningValue = settings
                };
            }
            catch (Exception ex)
            {
                return new CrudOperationResult<Settings>
                {
                    Success = false,
                    StatusCode = 500,
                    Message = ex.Message
                };
            }
        }

        public async Task<CrudOperationResult<Settings>> UpdateSettingsAsync(EditSettingsApiModel model)
        {
            try
            {
                var getSettingsResult = await GetSettingsAsync();            
                if (getSettingsResult.Success)
                {
                    var settings = getSettingsResult.ReturningValue;                    

                    settings.LogoUrl = model.logoUrl;
                    settings.FooterContent = model.footerContent;
       
                    db.Entry(settings).State = EntityState.Modified;
                    await db.SaveChangesAsync();

                    return new CrudOperationResult<Settings>
                    {
                        Success = true,
                        StatusCode = 200,
                        ReturningValue = settings,
                        Message = ReturningMessages.UpdateSuccessful(settings)
                    };                    
                }
                else
                {
                    return new CrudOperationResult<Settings>
                    {
                        Success = false,
                        StatusCode = 404,
                        Message = ReturningMessages.NotFound(getSettingsResult.ReturningValue)
                    };                 
                }
            }
            catch (Exception ex)
            {
                return new CrudOperationResult<Settings>
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
