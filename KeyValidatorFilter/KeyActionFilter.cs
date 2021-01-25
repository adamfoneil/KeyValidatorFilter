using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AO.ActionFilters
{
    public class KeyActionFilter : IActionFilter
    {
        private readonly HashSet<string> _allowedKeys;
        
        public KeyActionFilter(HashSet<string> allowedKeys)
        {
            _allowedKeys = allowedKeys;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            // continue as usual
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var result = FilterCommon.ValidateKey(_allowedKeys, context);
            if (result != null) context.Result = result;
        }
    }
}
