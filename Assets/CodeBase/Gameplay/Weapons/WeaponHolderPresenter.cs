using CodeBase.Gameplay.Character.Players.Shoot;
using CodeBase.Services.Providers;
using CodeBase.UI.Weapons;
using UnityEngine;
using Zenject;

namespace CodeBase.Gameplay.Weapons
{
    public class WeaponHolderPresenter : MonoBehaviour
    {
        [SerializeField] private WeaponHolderView weaponHolderView;
        [SerializeField] private ShootingOnAnimationEvent _shootingOnAnimationEvent;
        
        private IProvider<Weapon> _weaponProvider;

        [Inject]
        private void Construct(IProvider<Weapon> weaponProvider)
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
            weaponHolderView.SetLastWeaponViewActive(true, _weaponProvider.Get().WeaponTypeId);

        private void DisableWeaponInHand() => 
            weaponHolderView.SetLastWeaponViewActive(false, _weaponProvider.Get().WeaponTypeId);
    }
}