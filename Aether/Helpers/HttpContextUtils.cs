using Aether.Extensions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Aether.Helpers
{
    public class HttpContextUtils : IHttpContextUtils
    {
        public async Task<string> PeekRequestBodyAsync(HttpContext ctx)
        {
            return await ctx.Request.PeekBodyAsync();
        }
    }
}
