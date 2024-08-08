using System;
using Launcher;
using UnityEngine;
using Utility;

namespace Clicker
{
    [Serializable]
    public class ClickerController
    {
        private readonly ScoreController _scoreController;
        
        private readonly TransitionData _transitionData;
        
        [SerializeField] 
        private int _currentScore;

        [NonSerialized]
        public Action<int> OnScoreChanged = delegate { };

        private ClickerController(ScoreController scoreController,
            TransitionData transitionData)
        {
            _scoreController = scoreController;

            _transitionData = transitionData;
        }

        public void IncreaseScore()
        {
            _currentScore++;
            
            OnScoreChanged?.Invoke(_currentScore);
        }

        public void SaveScore()
        {
            _scoreController.SaveScore(_currentScore, _transitionData.SaveFileName);
        }

        public void LoadScore()
        {
            _currentScore = _scoreController.LoadScore(_transitionData.SaveFileName);
            
            OnScoreChanged?.Invoke(_currentScore);
        }
    }
}