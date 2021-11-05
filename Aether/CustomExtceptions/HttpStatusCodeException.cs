using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Aether.CustomExtceptions
{
    public class HttpStatusCodeException : Exception
    {
        public HttpStatusCode StatusCode { get; set; }

        public HttpStatusCodeException(HttpStatusCode statusCode, string message) : base (message)
        {
            StatusCode = statusCode;
        }
    }
}
