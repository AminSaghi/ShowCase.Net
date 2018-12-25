﻿using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShowCase.Api.Attributes
{
    public class GenerateApiAntiForgeryTokenAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if (string.Equals(context.HttpContext.Request.Method, "GET", StringComparison.OrdinalIgnoreCase))
            {
                var antiforgery = (IAntiforgery)context.HttpContext.RequestServices.GetService(typeof(IAntiforgery));

                /* 
                 * We can send the request token as a JavaScript-readable cookie,
                 * and Angular will use it by default. 
                 */
                var tokens = antiforgery.GetAndStoreTokens(context.HttpContext);
                context.HttpContext.Response.Cookies.Append(
                    "XSRF-TOKEN",
                    tokens.RequestToken,
                    new CookieOptions() { HttpOnly = false });
            }            
        }
    }
}
