using System.Threading.Tasks;
using Aether.Extensions;
using Microsoft.AspNetCore.Http;

namespace Aether.Helpers
{
    public class HttpContextUtils : IHttpContextUtils
    {
        public async Task<string> PeekRequestBodyAsync(HttpContext ctx) =>
            await ctx.Request.PeekBodyAsync();
    }
}
