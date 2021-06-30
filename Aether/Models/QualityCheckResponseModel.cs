﻿using Aether.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aether.Models
{
    public class QualityCheckResponseModel
    {
        public string Name { get; set; }

        public bool CheckPassed 
        { 
            get 
            {
                return Steps.IsNullOrEmpty() ? false
                                             : Steps.TrueForAll(s => s.StepPassed);
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
