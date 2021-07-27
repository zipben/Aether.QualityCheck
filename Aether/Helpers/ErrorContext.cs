using Aether.Models.ErisClient;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aether.Helpers.Interfaces;

namespace Aether.Helpers
{
    public class ErrorContext : IErrorContext
    {
        private IHttpContextAccessor _httpContextAccessor;
        private string Key = "Errors";
        public ErrorContext(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public void CaptureMethod(IEnumerable<CorrelatedIdentifierResponseModel> responses)
        {
            _httpContextAccessor.HttpContext.Items.TryGetValue(Key, out object value);
            var output = (List<CorrelatedIdentifierResponseModel>) value;
            if(output == null)
            {
                output = new List<CorrelatedIdentifierResponseModel>();
            }
            foreach (var response in responses)
            {
                output.Add(response);
            }
            var input = new KeyValuePair<object, object>(Key, output);
            _httpContextAccessor.HttpContext.Items.Add(input);
        }

        public List<CorrelatedIdentifierResponseModel> GetAllErrors()
        {
            _httpContextAccessor.HttpContext.Items.TryGetValue(Key, out object value);
            var output = (List<CorrelatedIdentifierResponseModel>) value;

            return output;
        }
    }
}
