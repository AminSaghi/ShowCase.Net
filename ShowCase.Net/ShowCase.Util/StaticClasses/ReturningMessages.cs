using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShowCase.Util.StaticClasses
{
    public static class ReturningMessages
    {
        #region Auth

        public static string InvalidUserNameOrPassword => "Invalid username or password.";

        public static string PasswordChangedSuccessfully => "Password changed successfully.";

        public static string UserExists => "A user with same username exists.";

        public static string IdentityResultErrors(IdentityResult identityResult)
        {
            return string.Join("\n",
                identityResult.Errors
                    .Select(e => e.Description)
                    .ToArray());
        }

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

        public static string ModelStateErrors(ModelStateDictionary modelState)
        {
            return string.Join("\n* ",
                modelState.Values
                    .SelectMany(me => me.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToArray());
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
