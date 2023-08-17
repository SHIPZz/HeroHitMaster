using TMPro;
using UnityEngine;

namespace CodeBase.UI.Windows.Shop
{
    public class ShopMoneyText : MonoBehaviour
    {
        private TextMeshProUGUI _text;

        private void Awake()
        {
            _text = GetComponent<TextMeshProUGUI>();
        }

        public void SetMoney(int money) =>
            _text.text = money.ToString();
    }
}