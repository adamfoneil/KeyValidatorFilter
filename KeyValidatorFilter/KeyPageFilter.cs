using Microsoft.AspNetCore.Mvc.Filters;
using System.Collections.Generic;

namespace AO.ActionFilters
{
    public class KeyPageFilter : IPageFilter
    {
        private readonly HashSet<string> _allowedKeys;

        public KeyPageFilter(HashSet<string> allowedKeys)
        {
            _allowedKeys = allowedKeys;
        }

        public void OnPageHandlerExecuted(PageHandlerExecutedContext context)
        {
            // behave as usual
        }

        public void OnPageHandlerExecuting(PageHandlerExecutingContext context)
        {
            var result = FilterCommon.ValidateKey(_allowedKeys, context);
            if (result != null) context.Result = result;
        }

        public void OnPageHandlerSelected(PageHandlerSelectedContext context)
        {
            // behave as usual
        }
    }
}
