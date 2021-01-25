using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AO.ActionFilters
{
    public class KeyValidatorFilter : IActionFilter
    {
        private readonly HashSet<string> _allowedKeys;
        
        private const string queryStringKey = "key";

        public KeyValidatorFilter(HashSet<string> allowedKeys)
        {
            _allowedKeys = allowedKeys;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            // continue as usual
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            try
            {
                var attributes = context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>();
                if (attributes.Any()) return;

                var key = context.HttpContext.Request.Query[queryStringKey];
                if (_allowedKeys.Contains(key)) return;
                throw new Exception("key missing");
            }
            catch
            {
                // any exception here means key was missing or invalid
                context.Result = new BadRequestObjectResult(new
                {
                    message = "Invalid or missing Request key value"
                });
            }
        }
    }
}
