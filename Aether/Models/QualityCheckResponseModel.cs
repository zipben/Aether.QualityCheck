using System.Collections.Generic;
using System.Linq;

namespace Aether.QualityChecks.Models
{
    public class QualityCheckResponseModel
    {
        public string Name { get; set; }

        public bool CheckPassed 
        {
            get
            {
                if (Steps is null || Steps.Count == 0)
                    return false;
                else
                    return Steps.All(s => s.StepPassed);
            }
        }

        public List<StepResponse> Steps { get; set; }

        public QualityCheckResponseModel(string name)
        {
            Name = name;
            Steps = new List<StepResponse>();
        }
    }
}
