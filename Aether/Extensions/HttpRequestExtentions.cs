using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Aether.Extensions
{
    public static class HttpRequestExtentions
    {
        public static bool IsTest(this HttpRequest request)
        {
            return request.Headers.ContainsKey(Constants.IS_TEST_HEADER_KEY);
        }

        public static async Task<string> PeekBodyAsync(this HttpRequest request, Encoding encoding = null)
        {
            try
            {
                if (encoding == null) 
                    encoding = new UTF8Encoding();

                request.EnableBuffering();

                var buffer = new byte[Convert.ToInt32(request.ContentLength)];

                if (buffer.Length == 0) 
                    return "";

                await request.Body.ReadAsync(buffer, 0, buffer.Length);

                return encoding.GetString(buffer);
            }
            finally
            {
                request.Body.Position = 0;
            }
        }
    }
}
