using Cysharp.Threading.Tasks;
using Launcher.UI;
using Utility;
using VContainer;
using VContainer.Unity;

namespace Launcher
{
    public class LauncherController : ILoadingUnit
    {
        private readonly IObjectResolver _resolver;
        
        private readonly LauncherSceneReferences _launcherSceneReferences;
        
        private LauncherController(IObjectResolver resolver, 
            LauncherSceneReferences launcherSceneReferences)
        {
            _resolver = resolver;
            
            _launcherSceneReferences = launcherSceneReferences;
        }

        public UniTask Load()
        {
            foreach (var config in _launcherSceneReferences.Configs)
            {
                var gameLoader = _resolver.Instantiate(_launcherSceneReferences.GameLoaderPrefab, 
                    _launcherSceneReferences.GameLoaderParent);
                
                gameLoader.SetupGameLoader(config);
            }
            
            return UniTask.CompletedTask;
        }
    }
}