using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.ObjectBuilder;
using Trial.Scheduler.Core.Validation;

namespace Trial.Scheduler.Core.Unity
{
    public class SchedulerUnityExtension : UnityContainerExtension
    {
        protected override void Initialize()
        {
            Context.Strategies.Add(new AutoResolutionPropertyStrategy(Container, typeof(IValidatorLocator), NullValidatorLocator.Instance), UnityBuildStage.Initialization);
            Context.Strategies.Add(new EnumerableResolutionStrategy(), UnityBuildStage.TypeMapping);
        }
    }
}