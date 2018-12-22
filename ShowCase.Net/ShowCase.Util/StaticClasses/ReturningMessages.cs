using System;
using System.Collections.Generic;
using System.Text;

namespace ShowCase.Util.StaticClasses
{
    public static class ReturningMessages
    {
        #region Auth

        public static string InvalidUserNameOrPassword => "Invalid username or password.";

        #endregion

        #region CUD

        public static string CreateSuccessful(object entity)
        {
            return CudSuccessful("created", entity);
        }
        public static string UpdateSuccessful(object entity)
        {
            return CudSuccessful("updated", entity);
        }
        public static string DeleteSuccessful(object entity)
        {
            return CudSuccessful("deleted", entity);
        }

        #endregion        

        #region Errors

        public static string InvalidDataSupplied => "Invalid data supplied.";

        public static string NotFound(object entity)
        {
            return $"{GetTypeName(entity)} NOT found.";
        }                

        #endregion

        #region Private Methods

        private static string CudSuccessful(string verb, object entity)
        {
            return $"{GetTypeName(entity)} {verb} successfully.";
        }

        private static string GetTypeName(object obj)
        {
            return obj.GetType().Name;
        }

        #endregion
    }
}
