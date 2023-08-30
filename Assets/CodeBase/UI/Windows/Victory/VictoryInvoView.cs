using DG.Tweening;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace CodeBase.UI.Windows.Victory
{
    public class VictoryInvoView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _deadEnemyQuantity;
        [SerializeField] private TextMeshProUGUI _earnedMoney;

        private void Awake()
        {
            _deadEnemyQuantity.text = "";
            _earnedMoney.text = "";
        }

        public void Show(int targetEnemyCount, int targetMoneyCount)
        {
            AnimateTextChange(targetEnemyCount, _deadEnemyQuantity);
            AnimateTextChange(targetMoneyCount, _earnedMoney);
        }

        private void AnimateTextChange(int targetValue, TMP_Text textField)
        {
            int startValue = 0;
            float animationDuration = 1f;

            DOTween.To(() => startValue, x =>
            {
                startValue = x;
                textField.text = startValue.ToString();
            }, targetValue, animationDuration);

            DOTween
                .To(() => textField.fontSize, x => textField.fontSize = x, 65, 2f)
                .OnComplete(() => DOTween.To(() => textField.fontSize, x => textField.fontSize = x, 45, 1f));
        }
    }
}
