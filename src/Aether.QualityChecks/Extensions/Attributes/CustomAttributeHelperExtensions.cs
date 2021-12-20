using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Aether.QualityChecks.Extensions.MethodExtensions
{
    public static class CustomAttributeHelper
    {
        public static MethodInfo GetMethodWithAttribute<TAttribute>(this Type type)
        {
            return type.GetMethodsWithAttribute<TAttribute>().FirstOrDefault();
        }

        public static IEnumerable<MethodInfo> GetMethodsWithAttribute<TAttribute>(this Type type)
        {
            var methods = type.GetMethods();
            return methods.Where(m => m.GetCustomAttributes(typeof(TAttribute), true).Any());
        }

        public static IEnumerable<T> GetAttribute<T>(this MethodInfo method) where T : Attribute
        {
            return Attribute.GetCustomAttributes(method, typeof(T)).Select(a => a as T);
        }
    }
}
