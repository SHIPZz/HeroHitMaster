using CodeBase.Gameplay.Character.Players.Shoot;
using CodeBase.Services.Providers;
using CodeBase.Services.Storages;
using CodeBase.UI.Weapons;
using UnityEngine;
using Zenject;

namespace CodeBase.Gameplay.Weapons
{
    public class WeaponHolderPresenter : MonoBehaviour
    {
        [SerializeField] private WeaponHolderView weaponHolderView;
        [SerializeField] private ShootingOnAnimationEvent _shootingOnAnimationEvent;
        
        private WeaponProvider _weaponProvider;

        [Inject]
        private void Construct(WeaponProvider weaponProvider)
        {
            _weaponProvider = weaponProvider;
        }

        private void OnEnable()
        {
            _shootingOnAnimationEvent.Shooted += DisableWeaponInHand;
            _shootingOnAnimationEvent.Stopped += EnableWeaponInHand;
        }

        private void OnDisable()
        {
            _shootingOnAnimationEvent.Shooted -= DisableWeaponInHand;
            _shootingOnAnimationEvent.Stopped -= EnableWeaponInHand;
        }

        private void EnableWeaponInHand() => 
            weaponHolderView.SetLastWeaponViewActive(true, _weaponProvider.CurrentWeapon.WeaponTypeId);

        private void DisableWeaponInHand() => 
            weaponHolderView.SetLastWeaponViewActive(false, _weaponProvider.CurrentWeapon.WeaponTypeId);
    }
}