using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aether.Extensions
{
    public static class HttpRequestExtentions
    {
        public static bool IsTest(this HttpRequest request)
        {
            return request.Headers.ContainsKey(Constants.IS_TEST_HEADER_KEY);
        }
    }
}
