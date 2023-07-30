using CodeBase.Gameplay.Character.Players.Shoot;
using DG.Tweening;
using Enums;
using UnityEngine;
using Weapons;
using Zenject;

namespace CodeBase.Services.Storages
{
    public class PlayerWeaponInHandPresenter : MonoBehaviour
    {
        [SerializeField] private WeaponViewStorage _weaponViewStorage;
        [SerializeField] private ShootingOnAnimationEvent _shootingOnAnimationEvent;
        
        private WeaponSelector _weaponSelector;

        [Inject]
        private void Construct(WeaponSelector weaponSelector)
        {
            _weaponSelector = weaponSelector;
        }

        private void OnEnable()
        {
            _weaponSelector.NewWeaponChanged += EnableWeaponInHand;
            _shootingOnAnimationEvent.Shooted += DisableWeaponInHand;
            _shootingOnAnimationEvent.Stopped += EnableWeaponInHand;
        }

        private void OnDisable()
        {
            _weaponSelector.NewWeaponChanged -= EnableWeaponInHand;
            _shootingOnAnimationEvent.Shooted -= DisableWeaponInHand;
            _shootingOnAnimationEvent.Stopped -= EnableWeaponInHand;
        }

        private void EnableWeaponInHand() =>
            _weaponViewStorage.SetLastWeaponViewActive(true);

        private void DisableWeaponInHand() =>
            _weaponViewStorage.SetLastWeaponViewActive(false);

        private void EnableWeaponInHand(WeaponTypeId weaponTypeId)
        {
            if (_weaponViewStorage.Get(weaponTypeId) is null)
                _shootingOnAnimationEvent.gameObject.SetActive(false);
            
            _weaponViewStorage.Get(weaponTypeId);
        }
    }
}