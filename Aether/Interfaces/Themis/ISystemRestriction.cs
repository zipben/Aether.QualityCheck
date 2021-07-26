﻿using System;

namespace Aether.Interfaces.Themis
{
    public interface ISystemRestriction
    {
        public string SystemId { get; set; }
        public string SystemName { get; set; }
        public string Id { get; set; }
        public bool IsRestricted { get; set; }
        public int CreatedById { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastUpdatedDate { get; set; }
        public int LastUpdatedById { get; set; }
    }
}