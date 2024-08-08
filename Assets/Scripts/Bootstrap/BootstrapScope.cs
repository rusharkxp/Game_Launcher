using Launcher;
using Utility;
using VContainer;
using VContainer.Unity;

namespace Bootstrap
{
    public class BootstrapScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<LoadingService>(Lifetime.Scoped);

            builder.Register<SceneLoadingService>(Lifetime.Scoped);

            builder.Register<ResourceLoadingService>(Lifetime.Scoped);
            
            builder.Register<ScoreController>(Lifetime.Scoped);
            
            builder.Register<TransitionData>(Lifetime.Singleton);

            builder.RegisterEntryPoint<BootstrapFlow>();
        }
        
        protected override void Awake()
        {
            base.Awake();
            
            DontDestroyOnLoad(this);
        }
    }
}