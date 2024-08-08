using Launcher.UI;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Launcher.Scope
{
    public class LauncherScope : LifetimeScope
    {
        [SerializeField] private LauncherUILoading _launcherUILoading;
        
        [SerializeField] private LauncherSceneReferences _LauncherSceneReferences;
        
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterComponent(_launcherUILoading);
            
            builder.RegisterComponent(_LauncherSceneReferences);
            
            builder.Register<LauncherController>(Lifetime.Scoped);
            
            builder.RegisterEntryPoint<LauncherFlow>();
        }
    }
}