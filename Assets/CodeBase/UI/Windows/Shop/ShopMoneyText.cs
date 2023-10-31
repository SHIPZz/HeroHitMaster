using TMPro;
using UnityEngine;

namespace CodeBase.UI.Windows.Shop
{
    public class ShopMoneyText : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;

        public void SetMoney(int money) =>
            _text.text = money.ToString();
    }
}