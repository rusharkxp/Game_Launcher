using Cysharp.Threading.Tasks;
using Launcher;
using UnityEngine;
using Utility;
using VContainer;

namespace Walker.Controllers
{
    public class WalkerScoreController : ILoadingUnit
    {
        private readonly ScoreController _scoreController;

        private readonly TransitionData _transitionData;

        private int _currentHighScore;

        private int _currentScore;
        
        [Inject]
        private WalkerScoreController(ScoreController scoreController,
             TransitionData transitionData)
        {
            _scoreController = scoreController;

            _transitionData = transitionData;
        }

        public void SaveHighScore(float currentScore)
        {
            _currentScore = Mathf.RoundToInt(CalculateScore(currentScore));
            
            if (_currentHighScore > _currentScore)
            {
                return;
            }
            
            _scoreController.SaveScore(_currentScore, _transitionData.SaveFileName);
        }

        public int GetCurrentScore()
        {
            return _currentScore;
        }

        public int GetHighScore()
        {
            return _currentHighScore;
        }
        
        public UniTask Load()
        {
            LoadHighScore();
            
            return UniTask.CompletedTask;
        }

        private static float CalculateScore(float currentScore)
        {
            return 1000 / currentScore + 100;
        }

        private void LoadHighScore()
        {
            _currentHighScore = _scoreController.LoadScore(_transitionData.SaveFileName);
        }
    }
}