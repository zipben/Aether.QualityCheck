﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Aether.Models
{
    public abstract class BaseEmailSendModel
    {
        public string TemplateId { get; set; }
        public string Stage { get; set; }
        public string ApplicationId { get; set; }
        public string From { get; set; }
        public List<string> To { get; set; }
        public List<string> CC { get; set; }
        public string Subject { get; set; }
    }
}