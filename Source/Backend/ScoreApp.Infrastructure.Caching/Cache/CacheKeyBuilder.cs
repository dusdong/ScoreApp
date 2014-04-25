using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace ScoreApp.Infrastructure.Caching
{
    internal class CacheKeyBuilder : ICacheKeyParameters, ICacheKeyBuilder
    {
        private Type classType;
        private string methodName;
        private ICollection<object> parameters;

        public ICacheKeyParameters Create(Type classType, [CallerMemberName]string methodName = "")
        {
            this.classType = classType;
            this.methodName = methodName;

            return this;
        }

        ICacheKeyBuilder ICacheKeyParameters.With(params object[] parameters)
        {
            this.parameters = parameters.ToList();
            return this;
        }

        ICacheKeyBuilder ICacheKeyParameters.WithoutParameters()
        {
            return this;
        }

        string ICacheKeyBuilder.Build()
        {
            var cacheKey = new CacheKey(classType, methodName, GetParameters());

            return cacheKey.ToString();
        }

        private object[] GetParameters()
        {
            return parameters.ToArray();
        }
    }
}
