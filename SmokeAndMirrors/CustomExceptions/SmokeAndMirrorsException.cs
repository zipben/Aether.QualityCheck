using Aether.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmokeAndMirrors.CustomExceptions
{
    [CustomExceptionReponse(System.Net.HttpStatusCode.AlreadyReported)]
    public class SmokeAndMirrorsException : Exception 
    {
        public SmokeAndMirrorsException(string error) : base(error) { }
    }
}
