using CodeBase.Services.Data;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace CodeBase.Gameplay.Bullet
{
    public class KnifeMovement : MonoBehaviour, IBulletMovement
    {
        private BulletStaticDataService _bulletStaticDataService;

        [Inject]
        public void Construct(BulletStaticDataService bulletStaticDataService)
        {
            _bulletStaticDataService = bulletStaticDataService;
        }

        public void Move(Vector3 target, Bullet bullet, Vector3 startPosition)
        {
            var rotateDuration = _bulletStaticDataService.GetBy(bullet.BulletTypeId).RotateDuration;
            var moveDuration = _bulletStaticDataService.GetBy(bullet.BulletTypeId).MovementDuration;

            Vector3 direction = target - startPosition;

            SetMove(target, bullet, startPosition, direction, moveDuration);

            Rotate(bullet, rotateDuration);
        }

        private void SetMove(Vector3 target, Bullet bullet, Vector3 startPosition, Vector3 direction,
            float moveDuration)
        {
            bullet.transform.forward = direction;
            bullet.transform.position = startPosition;
            bullet.transform.DOMove(target, moveDuration);
        }

        private void Rotate(Bullet bullet, float rotateDuration)
        {
            var throwBullet = bullet as ThrowingBullet;
            
            Vector3 startTargetRotation = new Vector3(104, bullet.transform.localEulerAngles.y,
                bullet.transform.localEulerAngles.z);
            
            Vector3 flyRotation = new Vector3(360, 0, 0);
            
            throwBullet.BulletModel.DOLocalRotate(new Vector3(0, 95, 0), 0);

            throwBullet.transform
                .DORotate(startTargetRotation, 0f);

            throwBullet.transform
                .DORotate(flyRotation, rotateDuration).SetRelative(true).SetEase(Ease.Linear);
        }
    }
}