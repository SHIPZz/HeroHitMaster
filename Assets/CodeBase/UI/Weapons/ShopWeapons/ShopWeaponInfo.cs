using TMPro;
using UnityEngine;

namespace CodeBase.UI.Weapons.ShopWeapons
{
    public class ShopWeaponInfo : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _name;
        [SerializeField] private TextMeshProUGUI _price;

        public void SetInfo(string name, int price)
        {
            _name.text = name;
            _price.text = price.ToString();
        }
    }
}