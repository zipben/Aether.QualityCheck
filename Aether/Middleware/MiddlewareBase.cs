using System.Collections.Generic;
using System.Text.RegularExpressions;
using APILogger.Interfaces;
using Ardalis.GuardClauses;
using Microsoft.AspNetCore.Http;

namespace Aether.Middleware
{
    public abstract class MiddlewareBase
    {
        protected readonly RequestDelegate _next;
        protected readonly IApiLogger _logger;

        public MiddlewareBase(IApiLogger logger, RequestDelegate next)
        {
            _logger = Guard.Against.Null(logger, nameof(logger));
            _next =   Guard.Against.Null(next, nameof(next));
        }

        protected bool IsInFilter(HttpContext context, List<string> filterList)
        {
            foreach (var filter in filterList)
            {
                Regex rgx = new Regex(filter);

                if (rgx.IsMatch(context.Request.Path))
                    return true;
            }

            return false;
        }
    }
}
