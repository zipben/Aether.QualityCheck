﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Aether.Models
{
    public class TemplatedEmailSendModel : BaseEmailSendModel
    {
        public Dictionary<string, string> Contents { get; set; }
    }
}
