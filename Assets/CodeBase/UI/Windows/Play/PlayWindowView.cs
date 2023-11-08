using System.Collections.Generic;
using System.Linq;
using CodeBase.Enums;
using CodeBase.Services.Providers;
using CodeBase.UI.Weapons;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace CodeBase.UI.Windows.Play
{
    public class PlayWindowView : MonoBehaviour
    {
        private Dictionary<WeaponTypeId, Image> _playWeaponIcons;

        [Inject]
        private void Construct(IProvider<WeaponIconsProvider> provider) =>
            _playWeaponIcons = provider.Get().PlayWindowWeaponIcons.ToDictionary(x => x.WeaponTypeId, 
                x => x.GetComponent<Image>());

        public void SetCurrentWeapon(WeaponTypeId weaponTypeId)
        {
            foreach (Image image in _playWeaponIcons.Values)
            {
                image.gameObject.SetActive(false);
            }
            
            _playWeaponIcons[weaponTypeId].gameObject.SetActive(true);
        }
    }
}