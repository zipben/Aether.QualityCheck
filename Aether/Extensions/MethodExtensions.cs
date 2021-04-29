using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace Aether.Extensions
{
    public static class MethodExtensions
    {
        public static bool HasAttribute<T>(this MethodInfo method)
        {
            const bool includeInherited = false;
            return method.GetCustomAttributes(typeof(T), includeInherited).Any();
        }

        public static bool HasAttribute<T>(Expression<System.Action> expression)
        {
            MethodInfo method = MethodOf(expression);

            return method.HasAttribute<T>();
        }

        public static MethodInfo MethodOf(Expression<System.Action> expression)
        {
            MethodCallExpression body = (MethodCallExpression)expression.Body;
            return body.Method;
        }

    }
}
