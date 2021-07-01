using System.Collections.Generic;
using System.Linq;
using Aether.Extensions;

namespace Aether.Models
{
    public class QualityCheckResponseModel
    {
        public string Name { get; set; }

        public bool CheckPassed 
        { 
            get => Steps.IsNullOrEmpty() ? false
                                         : Steps.All(s => s.StepPassed);
        }

        public List<StepResponse> Steps { get; set; }

        public QualityCheckResponseModel(string name)
        {
            Name = name;
            Steps = new List<StepResponse>();
        }
    }
}
