using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Launcher.UI;
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
                
                progress.Report(handle.GetDownloadStatus());

                Addressables.Release(handle);
            }
            catch
            {
                progress.ReportError();

                return false;
            }

            return true;
        }
    
        public async UniTask UnloadResources(string bundleName, 
            LauncherUILoading launcherUILoading, CancellationToken cancellationToken)
        {
            try
            {
                var handle = Addressables.LoadResourceLocationsAsync(bundleName);

                await handle.WithCancellation(cancellationToken);

                if (handle.Status == AsyncOperationStatus.Succeeded)
                {
                    Addressables.Release(handle);

                    Addressables.ClearDependencyCacheAsync(bundleName);
                
                    launcherUILoading.Report(1f);
                
                    launcherUILoading.SetMessage($"Successfully unloaded - { bundleName.ToLower() }");
                }
            }
            catch (Exception)
            {
                launcherUILoading.ReportError($"An error occured while unloading - { bundleName.ToLower() }");
            }
        }

        public async UniTask<bool> BundleExists(string bundleName, 
            CancellationToken cancellationToken)
        {
            bool bundleExists;
            
            try
            {
                var handle = Addressables.GetDownloadSizeAsync(bundleName);

                await handle.WithCancellation(cancellationToken);

                bundleExists = handle is { Status: AsyncOperationStatus.Succeeded, Result: 0 };

            }
            catch (Exception)
            {
                return false;
            }

            return bundleExists;
        }
    }
}

