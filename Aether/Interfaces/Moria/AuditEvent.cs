﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Aether.Interfaces.Moria
{
    public interface IAuditEvent
    {
        public string EventId { get; set; }
        public string EventName { get; set; }
        public string SystemOfOrigin { get; set; }
        public string TargetId { get; set; }
        public long EventCreateDate { get; set; }
        public string EventInitiator { get; set; }
        public string OriginalValue { get; set; }
        public string NewValue { get; set; }
    }
}
