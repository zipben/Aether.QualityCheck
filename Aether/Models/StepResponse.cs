using System;
using System.Net;
using System.Threading.Tasks;

namespace Aether.QualityChecks.Models
{
    public class StepResponse
    {
        public string Name { get; set; }
        public string Message { get; set; }
        public bool StepPassed { get; set; }
        public HttpStatusCode? HttpStatus { get; set; }
        public Exception Exception { get; set; }
    }

    public delegate Task<StepResponse> Step(string testId);
}
