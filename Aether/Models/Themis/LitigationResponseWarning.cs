﻿using System.Collections.Generic;

namespace Aether.Models.Themis
{
    public class LitigationResponseWarning
    {
        public string CaseName { get; set; }
        public List<WarningMessage> Messages { get; set; }
    }
}