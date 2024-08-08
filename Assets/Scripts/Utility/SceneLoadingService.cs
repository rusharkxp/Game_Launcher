using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;

namespace Utility
{
    public class SceneLoadingService
    {
        private readonly ResourceLoadingService _resourceLoadingService;
        
        private SceneLoadingService(ResourceLoadingService resourceLoadingService)
        {
            _resourceLoadingService = resourceLoadingService;
        }

        public async UniTask LoadScene(string path)
        {
            await Addressables.LoadSceneAsync(path);
        }
    }
}