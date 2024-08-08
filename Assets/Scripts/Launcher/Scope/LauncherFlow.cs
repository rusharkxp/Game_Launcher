using Utility;
using VContainer;
using VContainer.Unity;

namespace Launcher.Scope
{
    public class LauncherFlow : IStartable
    {
        private readonly LoadingService _loadingService;

        private readonly LauncherController _launcherController;
        
        
        [Inject]
        private LauncherFlow(LoadingService loadingService,
            LauncherController launcherController)
        {
            _loadingService = loadingService;

            _launcherController = launcherController;
        }
        
        public async void Start()
        {
            await _loadingService.BeginLoading(_launcherController);
        }
    }
}