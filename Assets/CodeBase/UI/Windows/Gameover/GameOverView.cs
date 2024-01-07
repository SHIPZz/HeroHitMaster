using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Windows.Gameover
{
    public class GameOverView : MonoBehaviour
    {
        [SerializeField] private Image _background;
        [SerializeField] private TextMeshProUGUI _gameFinishText;
        [SerializeField] private float _targetAlpha = 1f;
        [SerializeField] private float _showUpDuration = 1.5f;
        [SerializeField] private float _disableDelay = 5f;
        [SerializeField] private Button _continueButton;

        public event Action Disabled;

        private void OnEnable() =>
            _continueButton.onClick.AddListener(Disable);

        private void OnDisable() =>
            _continueButton.onClick.RemoveListener(Disable);

        public void Init()
        {
            _background.DOFade(_targetAlpha, _showUpDuration).SetUpdate(true);
            _gameFinishText.DOFade(_targetAlpha, _showUpDuration).SetUpdate(true);
        }

        private void Disable() =>
            Disabled?.Invoke();
    }
}