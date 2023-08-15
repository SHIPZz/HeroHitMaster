using TMPro;
using UnityEngine;

namespace CodeBase.UI.Wallet
{
    public class WalletUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;

        public void SetValue(int money) =>
            _text.text = money.ToString();
    }
}