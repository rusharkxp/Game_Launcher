using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Clicker.Scope
{
    public class ClickerScope : LifetimeScope
    {
        [SerializeField] private ClickerUIController _clickerUIController;
        
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterComponent(_clickerUIController);
            
            builder.Register<ClickerController>(Lifetime.Scoped);

            builder.RegisterEntryPoint<ClickerFlow>();
        }
    }
}