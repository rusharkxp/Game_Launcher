using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Utility.UI
{
    public class UIButton : MonoBehaviour
    {
        [SerializeField] private Image _buttonImage;

        [SerializeField] private TextMeshProUGUI _buttonText;

        [SerializeField] private Color _enabledColor;
        
        [SerializeField] private Color _disabledColor;
        
        public event Action OnButtonClick;

        public void OnPointerClick(BaseEventData baseEventData) 
        {
            if (!_buttonImage.raycastTarget)
            {
                return;
            }

            if (((PointerEventData) baseEventData).button == PointerEventData.InputButton.Left) 
            {
                OnButtonClick?.Invoke();
            }
        }

        public void ToggleAvailability()
        {
            _buttonImage.raycastTarget = !_buttonImage.raycastTarget;

            SetButtonColor();
        }

        public void SetButtonText(string text)
        {
            _buttonText.text = text;
        }

        private void SetButtonColor()
        {
            _buttonImage.color = _buttonImage.raycastTarget ? _enabledColor : _disabledColor;
        }

        private void OnDestroy()
        {
            OnButtonClick = null;
        }
    }
}