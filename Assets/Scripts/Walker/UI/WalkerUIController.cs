using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Utility;
using VContainer;
using Walker.Controllers;
using CharacterController = Walker.Movement.CharacterController;

namespace Walker.UI
{
    public class WalkerUIController : MonoBehaviour, ILoadingUnit
    {
        [SerializeField] 
        private WalkerSceneReferences _walkerSceneReferences;

        private SceneLoadingService _sceneLoadingService;
        
        private WalkerTimer _walkerTimer;

        private CharacterController _characterController;

        private WalkerScoreController _walkerScoreController;
        
        private CancellationTokenSource _cancellationTokenSource;
        
        [Inject]
        private void Init(SceneLoadingService sceneLoadingService,
            WalkerTimer walkerTimer,
            CharacterController characterController,
            WalkerScoreController walkerScoreController)
        {
            _sceneLoadingService = sceneLoadingService;
            
            _walkerTimer = walkerTimer;

            _characterController = characterController;

            _walkerScoreController = walkerScoreController;
        }

        public UniTask Load()
        {
            _walkerTimer.OnTimerUpdate += OnTimerUpdate;

            _characterController.OnFinished += OnFinished;

            _walkerSceneReferences.BackButton.OnButtonClick += BackButtonClick;

            _cancellationTokenSource = new CancellationTokenSource();

            return UniTask.CompletedTask;
        }

        public void StartTimer()
        {
            _walkerTimer.Start(_cancellationTokenSource.Token);
        }

        private void OnTimerUpdate(float elapsedTime)
        {
            _walkerSceneReferences.TimerText.text = _walkerTimer.GetTimeString();
        }

        private void OnFinished()
        {
            _cancellationTokenSource.Cancel();
            
            _walkerScoreController.SaveHighScore(_walkerTimer.GetElapsedTime());
            
            _walkerSceneReferences.WalkerFinishMessage.ShowFinishMessage(
                _walkerScoreController.GetHighScore().ToString(),
                _walkerTimer.GetTimeString(),
                _walkerScoreController.GetCurrentScore().ToString());
        }

        private async void BackButtonClick()
        {
            await _sceneLoadingService.LoadScene(RuntimeConstants.LauncherSceneName);
        }

        private void OnApplicationQuit()
        {
            _cancellationTokenSource.Cancel();
            
            _cancellationTokenSource.Dispose();
        }
    }
}