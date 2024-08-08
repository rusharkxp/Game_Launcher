using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Launcher
{
    public class LauncherUILoading : MonoBehaviour, IProgress<float>
    {
        [SerializeField] private Image _loadingProgressImage;
        
        [SerializeField] private TextMeshProUGUI _loadingProgressText;
        
        public void Report(float value)
        {
            if (value < 0)
            {
                ReportError();
                
                return;
            }

            _loadingProgressImage.fillAmount = value;

            SetMessage($"Loading { (value < 1f ? ":" + value * 100 + "%" : "done!") }");
        }

        public void SetMessage(string text)
        {
            _loadingProgressText.text = text;
        }

        private void ReportError()
        {
            _loadingProgressImage.color = Color.red;

            SetMessage($"Loading Error!");
        }
    }
}