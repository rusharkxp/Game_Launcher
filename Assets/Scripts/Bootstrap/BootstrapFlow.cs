using Utility;
using VContainer.Unity;

namespace Bootstrap
{
    public class BootstrapFlow : IStartable
    {
        private readonly SceneLoadingService _sceneLoadingService;

        private BootstrapFlow(SceneLoadingService sceneLoadingService)
        {
            _sceneLoadingService = sceneLoadingService;
        }

        public async void Start()
        {
            await _sceneLoadingService.LoadScene("Launcher");
        }
    }
}