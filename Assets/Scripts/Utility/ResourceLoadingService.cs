using Cysharp.Threading.Tasks;
using Launcher;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Utility
{
    public sealed class ResourceLoadingService
    {
        public async UniTask<bool> LoadResources(string bundleName, LauncherUILoading progress)
        {
            try
            {
                var handle = Addressables.DownloadDependenciesAsync(bundleName);

                await handle.ToUniTask(progress);

                if (handle.GetDownloadStatus().DownloadedBytes == 0)
                {
                    progress.Report(1f);
                }
                
                progress.SetMessage($"Downloaded : {(handle.GetDownloadStatus().DownloadedBytes / 1000):F2} KB");

                Addressables.Release(handle);
            }
            catch
            {
                progress.Report(-1f);

                return false;
            }

            return true;
        }
    
        public async UniTask UnloadResources(string bundleName, LauncherUILoading launcherUILoading)
        {
            var handle = Addressables.LoadResourceLocationsAsync(bundleName);

            await handle.Task;

            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                Addressables.Release(handle);

                Addressables.ClearDependencyCacheAsync(bundleName);
                
                launcherUILoading.Report(1f);
                
                launcherUILoading.SetMessage($"Successfully unloaded - { bundleName.ToLower() }");
                
                return;
            }
            
            launcherUILoading.SetMessage($"An error occured while unloading - { bundleName.ToLower() }");
        }

        public async UniTask<bool> BundleExists(string bundleName)
        {
            bool bundleExists;
            
            try
            {
                var handle = Addressables.GetDownloadSizeAsync(bundleName);

                await handle.Task;

                bundleExists = handle is { Status: AsyncOperationStatus.Succeeded, Result: 0 };

            }
            catch (InvalidKeyException)
            {
                return false;
            }

            return bundleExists;
        }
    }
}

