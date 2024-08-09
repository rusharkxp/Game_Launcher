using TMPro;
using UnityEngine;

namespace Walker.UI
{
    public class WalkerFinishMessage : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _highScoreText;
        
        [SerializeField] private TextMeshProUGUI _timeText;
        
        [SerializeField] private TextMeshProUGUI _scoreText;

        public void ShowFinishMessage(string highScore, string time, string score)
        {
            gameObject.SetActive(true);
            
            _highScoreText.text = $"HS: {highScore}";

            _timeText.text = $"Time: {time}";

            _scoreText.text = $"Score: {score}";
        }
    }
}