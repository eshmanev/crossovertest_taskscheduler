using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;

namespace Trial.Scheduler.Core.Unity
{
    /// <summary>
    /// Represents a strategy used to automatically resolve value of the property.
    /// </summary>
    internal class AutoResolutionPropertyStrategy : IBuilderStrategy
    {
        private static readonly ConcurrentDictionary<Type, ConcurrentDictionary<Type, List<PropertyInfo>>> Cache = new ConcurrentDictionary<Type, ConcurrentDictionary<Type, List<PropertyInfo>>>();
        private readonly IUnityContainer _container;
        private readonly Type _propertyType;
        private readonly object _defaultValue;

        public AutoResolutionPropertyStrategy(IUnityContainer container, Type propertyType, object defaultValue)
        {
            Contract.Requires(container != null);
            Contract.Requires(propertyType != null);

            _container = container;
            _propertyType = propertyType;
            _defaultValue = defaultValue;
        }

        public void PreBuildUp(IBuilderContext context)
        {
        }

        public void PostBuildUp(IBuilderContext context)
        {
            if (context.Existing == null)
                return;

            var objectType = context.Existing.GetType();
            if (objectType == _propertyType)
                return;

            var propertyInfo = GetPropertyInfo(objectType, _propertyType);
            if (propertyInfo.Count > 0)
            {
                object value;
                if (!TryResolve(_propertyType, out value))
                    value = _defaultValue;
                
                foreach (var info in propertyInfo)
                {
                    info.SetValue(context.Existing, value);
                }
            }
        }

        public void PreTearDown(IBuilderContext context)
        {
        }

        public void PostTearDown(IBuilderContext context)
        {
        }

        private static List<PropertyInfo> GetPropertyInfo(Type type, Type propertyType)
        {
            ConcurrentDictionary<Type, List<PropertyInfo>> propertyMap;
            if (!Cache.TryGetValue(type, out propertyMap))
            {
                propertyMap = Cache[type] = new ConcurrentDictionary<Type, List<PropertyInfo>>();
            }

            List<PropertyInfo> propertyInfo;
            if (!propertyMap.TryGetValue(propertyType, out propertyInfo))
            {
                propertyInfo = propertyMap[propertyType] = type
                    .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                    .Where(x => x.PropertyType == propertyType && x.GetAccessors(false).Length == 2) // property must has public getter and setter
                    .ToList();
            }

            return propertyInfo;
        }

        private bool TryResolve(Type type, out object value)
        {
            try
            {
                value = _container.Resolve(type);
                return true;
            }
            catch
            {
                value = null;
                return false;
            }
        }
    }
}