using System.Threading;
using Cysharp.Threading.Tasks;
using Launcher.UI;
using Launcher.Utility;
using UnityEngine;
using Utility;
using VContainer;

namespace Launcher
{
    public class GameLoader : MonoBehaviour
    {
        [SerializeField] 
        private GameLoaderSceneReferences _gameLoaderSceneReferences;

        private SceneLoadingService _sceneLoadingService;
        
        private ResourceLoadingService _resourceLoadingService;

        private LauncherUILoading _launcherUILoading;

        private TransitionData _transitionData;
        
        private GameLoaderConfig _gameLoaderConfig;

        private CancellationTokenSource _cancellationTokenSource;
        
        [Inject]
        private void Init(SceneLoadingService sceneLoadingService, 
            ResourceLoadingService resourceLoadingService,
            LauncherUILoading launcherUILoading, TransitionData transitionData)
        {
            _sceneLoadingService = sceneLoadingService;
            
            _resourceLoadingService = resourceLoadingService;

            _launcherUILoading = launcherUILoading;

            _transitionData = transitionData;
        }

        public async void SetupGameLoader(GameLoaderConfig gameLoaderConfig)
        {
            _gameLoaderConfig = gameLoaderConfig;
            
            _gameLoaderSceneReferences.LoadResourcesButton.OnButtonClick += LoadResources;
            
            _gameLoaderSceneReferences.UnloadResourcesButton.OnButtonClick += UnloadResources;

            _gameLoaderSceneReferences.LoadGameButton.OnButtonClick += LoadGame;

            _gameLoaderSceneReferences.LoadGameButton.SetButtonText(_gameLoaderConfig.AssetName);

            _cancellationTokenSource = new CancellationTokenSource();
            
            await SetInitialButtonVisibility();
        }

        private async UniTask SetInitialButtonVisibility()
        {
            var result = await CheckBundle();
            
            if (result)
            {
                return;
            }

            _gameLoaderSceneReferences.UnloadResourcesButton.ToggleAvailability();
            
            _gameLoaderSceneReferences.LoadGameButton.ToggleAvailability();
        }

        private async UniTask<bool> CheckBundle()
        {
            if (!await _resourceLoadingService.BundleExists(_gameLoaderConfig.AssetName, _cancellationTokenSource.Token))
            {
                return false;
            }

            _gameLoaderSceneReferences.LoadResourcesButton.ToggleAvailability();

            return true;
        }

        private async void LoadResources()
        {
            if (!await _resourceLoadingService.LoadResources(_gameLoaderConfig.AssetName, _launcherUILoading))
            {
                return;
            }

            ToggleButtons();
        }

        private void ToggleButtons()
        {
            _gameLoaderSceneReferences.UnloadResourcesButton.ToggleAvailability();
            
            _gameLoaderSceneReferences.LoadGameButton.ToggleAvailability();
            
            _gameLoaderSceneReferences.LoadResourcesButton.ToggleAvailability();
        }

        private async void UnloadResources()
        {
            await _resourceLoadingService.UnloadResources(_gameLoaderConfig.AssetName,
                _launcherUILoading, _cancellationTokenSource.Token);
            
            ToggleButtons();
        }

        private async void LoadGame()
        {
            _transitionData.SaveFileName = _gameLoaderConfig.SaveFileName;

            await _sceneLoadingService.LoadScene(_gameLoaderConfig.AssetName);
        }

        private void OnDestroy()
        {
            _gameLoaderSceneReferences.LoadResourcesButton.OnButtonClick -= LoadResources;
            
            _gameLoaderSceneReferences.UnloadResourcesButton.OnButtonClick -= UnloadResources;

            _gameLoaderSceneReferences.LoadGameButton.OnButtonClick -= LoadGame;
        }

        private void OnApplicationQuit()
        {
            _cancellationTokenSource.Cancel();
            
            _cancellationTokenSource.Dispose();
        }
    }
}