using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Aether.Helpers
{
    public interface IHttpContextUtils
    {
        Task<string> PeekRequestBodyAsync(HttpContext ctx);
    }
}