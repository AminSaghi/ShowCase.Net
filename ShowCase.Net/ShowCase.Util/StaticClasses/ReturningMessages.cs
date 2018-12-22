using System;
using System.Collections.Generic;
using System.Text;

namespace ShowCase.Util.StaticClasses
{
    public static class ReturningMessages
    {
        #region HTTP 4xx

        public static string NotFound(object entity)
        {
            return $"{GetTypeName(entity)} NOT found.";
        }

        public static string InvalidDataSupplied()
        {
            return "Invalid data supplied.";
        }

        #endregion

        #region HTTP 2xx

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

        #region Auth

        public static string InvalidUserNameOrPassword => "Invalid username or password.";

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
