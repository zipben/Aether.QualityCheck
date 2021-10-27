using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Aether.Attributes
{
    public class CustomExceptionReponseAttribute : Attribute
    {
        public HttpStatusCode ReponseCode { get; set; }

        public CustomExceptionReponseAttribute(HttpStatusCode responseCode)
        {
            this.ReponseCode = responseCode;
        }
    }
}
