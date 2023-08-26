using System.Collections.Generic;
using CodeBase.Enums;
using CodeBase.Services.Providers;
using CodeBase.UI.Weapons;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace CodeBase.UI.Windows.Popup
{
    public class PopupInfoView : MonoBehaviour
    {
        private Dictionary<WeaponTypeId, WeaponSelectorView> _weaponIcons;

        [Inject]
        private void Construct(IProvider<WeaponIconsProvider> provider)
        {
            _weaponIcons = provider.Get().PopupIcons;
        }

        private void OnEnable()
        {
            List<WeaponSelectorView> randomIcons = GetRandomWeaponIcons(3);

            foreach (var icon in randomIcons)
            {
                icon.gameObject.SetActive(true);
            }
        }

        private List<WeaponSelectorView> GetRandomWeaponIcons(int count)
        {
            List<WeaponSelectorView> randomIcons = new List<WeaponSelectorView>();
    
            List<WeaponSelectorView> allIcons = new List<WeaponSelectorView>(_weaponIcons.Values);
            int totalIcons = allIcons.Count;

            for (int i = 0; i < count; i++)
            {
                if (totalIcons == 0)
                    break;

                int randomIndex = Random.Range(0, totalIcons);
                randomIcons.Add(allIcons[randomIndex]);
                allIcons.RemoveAt(randomIndex);
                totalIcons--;
            }

            return randomIcons;
        }
    }
}