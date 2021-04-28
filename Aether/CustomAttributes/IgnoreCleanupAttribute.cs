using System;
using System.Collections.Generic;
using System.Text;

namespace Aether.CustomAttributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class IgnoreCleanupAttribute : Attribute
    {
        
    }
    
}
