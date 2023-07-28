using DG.Tweening;
using Enums;
using Gameplay.Character.Players.Shoot;
using UnityEngine;
using Weapons;
using Zenject;

namespace Services.Storages
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

        public void Disable()
        {
            _weaponViewStorage.SetLastWeaponViewActive(false);
            DOTween.Sequence().AppendInterval(0.4f).OnComplete(() => _weaponViewStorage.SetLastWeaponViewActive(true));
            // DOTween.Sequence().AppendInterval(0.5f).OnComplete(() => _weaponViewStorage.SetLastWeaponViewActive(true));
        }

        private void EnableWeaponInHand(WeaponTypeId weaponTypeId)
        {
            if (_weaponViewStorage.Get(weaponTypeId) is null)
                _shootingOnAnimationEvent.gameObject.SetActive(false);
            
            _weaponViewStorage.Get(weaponTypeId);
        }
    }
}