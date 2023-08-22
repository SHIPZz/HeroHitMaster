using CodeBase.Services.Data;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace CodeBase.Gameplay.Bullet
{
    public class DefaultBulletMovement : BulletMovement
    {
        private BulletStaticDataService _bulletStaticDataService;

        [Inject]
        public void Construct(BulletStaticDataService bulletStaticDataService) => 
            _bulletStaticDataService = bulletStaticDataService;

        public override void Move(Vector3 target, Vector3 startPosition)
        {
            Vector3 direction = target - startPosition;

            SetMove(target, Bullet, startPosition, direction, MoveDuration);
            
            Bullet.transform.DOMove(startPosition, 0f).OnComplete(() =>
                RigidBody.DOMove(direction, 0.1f));
        }
        
        private void SetMove(Vector3 target, Bullet throwingBullet, Vector3 startPosition, Vector3 direction,
            float moveDuration)
        {
            throwingBullet.transform.forward = direction;
            throwingBullet.transform.position = startPosition;
            // throwingBullet.transform.DOMove(target, moveDuration);
        }
    }
}