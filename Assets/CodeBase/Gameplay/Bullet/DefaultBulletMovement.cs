using CodeBase.Services.Data;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace CodeBase.Gameplay.Bullet
{
    public class DefaultBulletMovement : MonoBehaviour, IBulletMovement
    {
        private BulletStaticDataService _bulletStaticDataService;

        [Inject]
        public void Construct(BulletStaticDataService bulletStaticDataService) => 
            _bulletStaticDataService = bulletStaticDataService;

        public void Move(Vector3 target, Bullet bullet, Vector3 startPosition)
        {
            var moveDuration = _bulletStaticDataService.GetBy(bullet.BulletTypeId).MovementDuration;
            
            bullet.transform.position = startPosition;
            bullet.transform.DOMove(target, moveDuration);
        }
    }
}