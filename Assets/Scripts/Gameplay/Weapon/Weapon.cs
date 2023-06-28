using DG.Tweening;
using Enums;
using Gameplay.Bullet;
using Gameplay.Web;
using Services.Factories;
using Services.Providers;
using UnityEngine;
using Zenject;

namespace Gameplay.Weapon
{
    public abstract class Weapon : MonoBehaviour
    {
        private const float ReturnBulletDelay = 1.5f;
        
        [field: SerializeField] public WeaponTypeId WeaponTypeId { get; protected set; }

        protected IBulletMovement BulletMovement = new DefaultBulletMovement();

        private WeaponsProvider _weaponsProvider;
        private BulletFactory _bulletFactory;
        
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
            BulletMovement.Move(target, bullet, initialPosition, 0.2f);

            DOTween.Sequence().AppendInterval(ReturnBulletDelay).OnComplete(() => _bulletFactory.Push(bullet));
        }
        
        private void Initialize()
        {
            _bulletFactory.CreateBulletsBy(WeaponTypeId, transform);
        }
    }
}