﻿using System;
using Aether.Enums;

namespace Aether.Interfaces.Themis
{
    public interface IIdentifierRestriction
    {
        public IdentifierType IdentifierType { get; set; }
        public string Id { get; set; }
        public bool IsRestricted { get; set; }
        public int CreatedById { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastUpdatedDate { get; set; }
        public int LastUpdatedById { get; set; }
    }
}