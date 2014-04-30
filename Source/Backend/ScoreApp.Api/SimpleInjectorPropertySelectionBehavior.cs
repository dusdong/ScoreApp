using SimpleInjector.Advanced;
using System;
using System.Linq;
using System.Reflection;

namespace ScoreApp.Api
{
    internal class SimpleInjectorPropertySelectionBehavior : IPropertySelectionBehavior
    {
        public bool SelectProperty(Type type, PropertyInfo prop)
        {
            return prop.GetCustomAttributes(typeof(SimpleInjectorPropertyAttribute)).Any();
        }
    }
}