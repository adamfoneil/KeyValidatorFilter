using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AO.ActionFilters
{
    internal static class FilterCommon
    {
        const string queryStringKey = "key";

        internal static ObjectResult ValidateKey(HashSet<string> allowedKeys, FilterContext context)
        {
            try
            {
                var attributes = context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>();
                if (attributes.Any()) return null;

                var key = context.HttpContext.Request.Query[queryStringKey];
                if (allowedKeys.Contains(key)) return null;
                throw new Exception("key missing");
            }
            catch
            {                
                return new BadRequestObjectResult(new
                {
                    message = "Invalid or missing Request key value"
                });
            }
        }
    }
}
