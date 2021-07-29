using Aether.Models.ErisClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aether.Helpers.Interfaces
{
    public interface IErrorContext
    {
        public void CaptureMethod(IEnumerable<CorrelatedIdentifierResponseModel> responses);

        public List<CorrelatedIdentifierResponseModel> GetAllErrors();
    }
}
