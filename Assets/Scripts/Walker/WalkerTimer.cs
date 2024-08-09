using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Walker
{
    public class WalkerTimer
    {
        private float _elapsedTime = 0f;
            
        private bool _isRunning;

        public Action<float> OnTimerUpdate = delegate { };
        
        public void Start(CancellationToken cancellationToken)
        {
            _isRunning = true;
            
            StartTimer(cancellationToken).Forget();
        }

        public float GetElapsedTime()
        {
            return _elapsedTime;
        }
        
        public string GetTimeString()
        {
            var minutes = Mathf.FloorToInt(_elapsedTime/ 60);
            
            var secs = Mathf.FloorToInt(_elapsedTime % 60);
            
            return $"{minutes:D2}:{secs:D2}";
        }

        private void Stop()
        {
            _isRunning = false;
        }

        private void Reset()
        {
            _elapsedTime = 0f;
                
            _isRunning = false;
        }

        private async UniTask StartTimer(CancellationToken cancellationToken)
        {
            try
            {
                while (_isRunning)
                {
                    _elapsedTime++;
                
                    OnTimerUpdate?.Invoke(_elapsedTime);
                
                    await UniTask.WaitForSeconds(1f, cancellationToken: cancellationToken);
                }
            }
            catch (OperationCanceledException)
            {
                Stop();
            }
        }
    }
}