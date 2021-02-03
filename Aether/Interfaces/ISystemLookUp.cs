﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Aether.Interfaces
{
    public interface ISystemLookup
    {
        public string SystemId { get; set; }
        public string SystemName { get; set; }
        public string EmailAddress { get; set; }
        public long CreatedDate { get; set; }
        public long LastUpdatedDate { get; set; }
    }
}