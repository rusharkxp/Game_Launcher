using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;

namespace Utility
{
    public class SceneLoadingService
    {
        public async UniTask LoadScene(string path)
        {
            await Addressables.LoadSceneAsync(path);
        }
    }
}