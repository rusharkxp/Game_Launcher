using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Utility
{
    public interface ILoadingUnit
    {
        public UniTask Load();
    }
    
    public sealed class LoadingService
    {
        private async UniTask OnFinishedLoading()
        {
            var currentThreadId = Thread.CurrentThread.ManagedThreadId;
            
            var mainThreadId = PlayerLoopHelper.MainThreadId;

            if (mainThreadId != currentThreadId) 
            {
               await UniTask.SwitchToMainThread();
            }
        }
        
        public async UniTask BeginLoading(ILoadingUnit loadingUnit)
        {
            try
            {
                await loadingUnit.Load();
            }
            catch (Exception e)
            {
                Debug.Log(e.Message + "\n" + e.StackTrace);
            }
            finally
            {
                await OnFinishedLoading();
            }
        }
    }
}