using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Windows.Victory
{
    public class VictoryInvoView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _deadEnemyQuantity;
        [SerializeField] private TextMeshProUGUI _earnedMoney;
        [SerializeField] private Button _continueButton;
        [SerializeField] private float _buttonWidth = 430;
        [SerializeField] private float _buttonHeight = 150;
        [SerializeField] private TMP_Text _continueText;
        
        private bool _textChanged;

        private void Awake()
        {
            _deadEnemyQuantity.text = "";
            _earnedMoney.text = "";
            _continueButton.interactable = false;
        }

        public async void Show(int targetEnemyCount, int targetMoneyCount)
        {
            AnimateTextChange(targetEnemyCount, _deadEnemyQuantity);
            AnimateTextChange(targetMoneyCount, _earnedMoney);

            while (!_textChanged)
            {
                await UniTask.Yield();
            }
            
            ShowUpContinueButton();
        }

        private void ShowUpContinueButton()
        {
            var rectTransform = _continueButton.GetComponent<RectTransform>();

            AnimateSizeDelta(rectTransform, _buttonHeight, _buttonWidth + 100, 0.5f, () =>
            {
                AnimateSizeDelta(rectTransform, _buttonHeight, _buttonWidth, 0.3f);
                _continueText.enabled = true;
                _continueButton.interactable = true;
            });

            AnimateSizeDelta(rectTransform, _buttonHeight + 100, _buttonWidth, .5f,
                () => { AnimateSizeDelta(rectTransform, _buttonHeight, _buttonWidth, 0.3f); });
        }

        private void AnimateSizeDelta(RectTransform rectTransform, float targetHeight, float targetWidth,
            float duration, TweenCallback onComplete = null)
        {
            DOTween.To(() => rectTransform.sizeDelta, size => { rectTransform.sizeDelta = size; },
                    new Vector2(targetWidth, targetHeight), duration)
                .OnComplete(onComplete).SetUpdate(true);
        }

        private void AnimateTextChange(int targetValue, TMP_Text textField)
        {
            int startValue = 0;
            float animationDuration = 1f;

            DOTween.To(() => startValue, x =>
            {
                startValue = x;
                textField.text = startValue.ToString();
            }, targetValue, animationDuration).SetUpdate(true);

            DOTween
                .To(() => textField.fontSize, x => textField.fontSize = x, 65, 2f)
                .OnComplete(() =>
                {
                    DOTween.To(() => textField.fontSize, x => textField.fontSize = x, 45, 1f)
                        .OnComplete(() => _textChanged = true).SetUpdate(true);
                }).SetUpdate(true);
        }
    }
}