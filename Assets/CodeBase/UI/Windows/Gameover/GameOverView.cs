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

        public event Action Disabled;

        public async void Init()
        {
            _background.DOFade(_targetAlpha, _showUpDuration).SetUpdate(true);
            _gameFinishText.DOFade(_targetAlpha, _showUpDuration).SetUpdate(true);

            await UniTask.WaitForSeconds(_disableDelay);

            Disabled?.Invoke();
        }
    }
}