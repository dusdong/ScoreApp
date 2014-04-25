using System;
using System.Linq;

namespace ScoreApp.Infrastructure.Caching
{
    internal class CacheKey
    {
        private readonly object[] arguments;
        private readonly Type classType;
        private readonly string methodName;

        public CacheKey(Type classType, string methodName, object[] arguments)
        {
            this.arguments = arguments ?? Enumerable.Empty<object>().ToArray();
            this.classType = classType;
            this.methodName = methodName;
        }

        public override string ToString()
        {
            return GetHashCode().ToString();
        }

        public override int GetHashCode()
        {
            int hash = 17;

            hash = hash * 31 + classType.GetHashCode();
            hash = hash * 31 + methodName.GetHashCode();

            for (int i = 0; i < arguments.Length; i++)
            {
                if (arguments[i] != null)
                    hash = hash * 31 + arguments[i].GetHashCode();
            }

            return hash;
        }
    }
}
