using Cysharp.Threading.Tasks;
using UnityEngine;
using Utility;
using VContainer;

namespace Clicker.UI
{
    public class ClickerUIController : MonoBehaviour, ILoadingUnit
    {
        [SerializeField] 
        private ClickerSceneReferences _clickerSceneReferences;

        private SceneLoadingService _sceneLoadingService;
        
        private ClickerController _clickerController;
        
        [Inject]
        private void Init(ClickerController clickerController,
            SceneLoadingService sceneLoadingService)
        {
            _clickerController = clickerController;

            _sceneLoadingService = sceneLoadingService;
        }

        public UniTask Load()
        {
            _clickerController.OnScoreChanged += UpdateScore;

            _clickerSceneReferences.ScoreUpButton.OnButtonClick += IncreaseScore;

            _clickerSceneReferences.BackButton.OnButtonClick += BackButton;
            
            _clickerController.LoadScore();
            
            return UniTask.CompletedTask;
        }

        private void IncreaseScore()
        {
            _clickerController.IncreaseScore();
        }

        private async void BackButton()
        {
            _clickerController.SaveScore();
            
            await _sceneLoadingService.LoadScene(RuntimeConstants.LauncherSceneName);
        }

        private void UpdateScore(int score)
        {
            _clickerSceneReferences.ScoreText.text = score.ToString();
        }

        private void OnDestroy()
        {
            _clickerController.OnScoreChanged -= UpdateScore;

            _clickerSceneReferences.ScoreUpButton.OnButtonClick -= IncreaseScore;

            _clickerSceneReferences.BackButton.OnButtonClick -= BackButton;
        }
    }
}