using System;
using System.Collections.Generic;
using System.Text;

namespace Ardalis.GuardClauses
{
    public static class DictionaryGuard
    {
        public static void MissingKey<T, TVal>(this IGuardClause guardClause, Dictionary<T, TVal> input, T key, string parameterName)
        {
            if (!input.ContainsKey(key))
                throw new ArgumentNullException(parameterName + "must be present in dictionary");
        }
    }
}
