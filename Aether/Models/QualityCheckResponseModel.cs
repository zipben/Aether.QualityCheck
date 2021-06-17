using Aether.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aether.Models
{
    public class QualityCheckResponseModel
    {
        public string Name { get; set; }

        public bool CheckPassed 
        { get 
            {
                if (Steps.IsNullOrEmpty())
                    return false;

                return Steps.TrueForAll(s => s.StepPassed); 
            } 
        }

        public List<StepResponse> Steps { get; set; }

        public QualityCheckResponseModel(string name)
        {
            Name = name;
            Steps = new List<StepResponse>();
        }
    }

    public class StepResponse
    {
        public string Name { get; set; }
        public string Message { get; set; }
        public bool StepPassed { get; set; }
    }
}
