using System.Collections;
using Microsoft.Practices.ObjectBuilder2;

namespace Trial.Scheduler.Core.Unity
{
    internal class EnumerableResolutionStrategy : IBuilderStrategy
    {
        public void PreBuildUp(IBuilderContext context)
        {
            if (context.BuildKey.Type.IsGenericType &&
                context.BuildKey.Type.FullName.StartsWith("System.Collections.Generic.IEnumerable") &&
                typeof (IEnumerable).IsAssignableFrom(context.BuildKey.Type))
            {
                var arrayType = context.BuildKey.Type.GenericTypeArguments[0].MakeArrayType();
                context.BuildKey = new NamedTypeBuildKey(arrayType, context.BuildKey.Name);
            }
        }

        public void PostBuildUp(IBuilderContext context)
        {
        }

        public void PreTearDown(IBuilderContext context)
        {
        }

        public void PostTearDown(IBuilderContext context)
        {
        }
    }
}