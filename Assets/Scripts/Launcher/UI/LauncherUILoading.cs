using System;
using TMPro;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

namespace Launcher.UI
{
    public class LauncherUILoading : MonoBehaviour, IProgress<float>
    {
        [SerializeField] private Image _loadingProgressImage;
        
        [SerializeField] private TextMeshProUGUI _loadingProgressText;
        
        public void Report(float value)
        {
            _loadingProgressImage.fillAmount = value;
        }

        public void Report(DownloadStatus downloadStatus)
        {
            if (downloadStatus.DownloadedBytes == 0f)
            {
                Report(1f);
            }

            SetMessage($"Downloaded : {downloadStatus.DownloadedBytes / 1000:F2} KB");
        }

        public void ReportError(string message = "Loading Error!")
        {
            Report(1f);

            SetMessage(message);
            
            _loadingProgressImage.color = Color.red;
        }

        public void SetMessage(string text)
        {
            _loadingProgressText.text = text;
        }
    }
}