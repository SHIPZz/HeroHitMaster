using Databases;
using DG.Tweening;
using Enums;
using Gameplay.Web;
using Services.Factories;
using UnityEngine;
using Zenject;

namespace Gameplay.Character.Player.Shoot
{
    public class ShootHand : MonoBehaviour, IWeapon
    {
        private const float ReleaseBulletDelay = 1.5f;

        [SerializeField] private WeaponTypeId _weaponTypeId;

        private readonly IBulletMovement _bulletMovement = new WebMovement();
        private WeaponsProvider _weaponsProvider;
        private BulletFactory _bulletFactory;

        public WeaponTypeId WeaponTypeId => _weaponTypeId;

        public GameObject GameObject =>
            gameObject;

        [Inject]
        private void Construct(WeaponsProvider weaponsProvider,
            BulletFactory bulletFactory)
        {
            _weaponsProvider = weaponsProvider;
            _bulletFactory = bulletFactory;
            Initialize();
        }

        public void Shoot(Vector3 target, Vector3 initialPosition)
        {
            IBullet bullet = _bulletFactory.Pop();
            _bulletMovement.Move(target, bullet, initialPosition, 0.2f);

            DOTween.Sequence().AppendInterval(ReleaseBulletDelay).OnComplete(() => _bulletFactory.Push(bullet));
        }

        private void Initialize()
        {
            _bulletFactory.CreateBulletsBy(_weaponTypeId);
            _weaponsProvider.CurrentWeapon = this;
            _weaponsProvider.Add(this);
            Debug.Log("initalized shoothand");
        }
        
        public class Factory : PlaceholderFactory<WeaponTypeId, ShootHand> { }
    }
}