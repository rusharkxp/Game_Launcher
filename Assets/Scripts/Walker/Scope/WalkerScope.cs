using UnityEngine;
using VContainer;
using VContainer.Unity;
using Walker.Controllers;
using Walker.UI;
using CharacterController = Walker.Movement.CharacterController;

namespace Walker.Scope
{
    public class WalkerScope : LifetimeScope
    {
        [SerializeField] private WalkerUIController _walkerUIController;
        
        [SerializeField] private CharacterController _characterController;
        
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterComponent(_walkerUIController);
            
            builder.RegisterComponent(_characterController);

            builder.Register<WalkerTimer>(Lifetime.Scoped);
            
            builder.Register<WalkerScoreController>(Lifetime.Scoped);
            
            builder.RegisterEntryPoint<WalkerFlow>();
        }
    }
}