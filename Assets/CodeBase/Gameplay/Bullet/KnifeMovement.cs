using System.Collections.Generic;
using CodeBase.Services.Data;
using DG.Tweening;
using UnityEngine;
using Zenject;
using Random = System.Random;

namespace CodeBase.Gameplay.Bullet
{
    public class KnifeMovement : MonoBehaviour, IBulletMovement
    {
        private static readonly Random _random = new();

        private BulletStaticDataService _bulletStaticDataService;

        [Inject]
        public void Construct(BulletStaticDataService bulletStaticDataService)
        {
            _bulletStaticDataService = bulletStaticDataService;
        }

        public void Move(Vector3 target, IBullet bullet, Vector3 startPosition, Rigidbody rigidbody)
        {
            var rotateDuration = _bulletStaticDataService.GetBy(bullet.BulletTypeId).RotateDuration;
            var moveDuration = _bulletStaticDataService.GetBy(bullet.BulletTypeId).MovementDuration;
            
            Vector3 direction = target - startPosition;
            bullet.GameObject.transform.forward = direction;
            bullet.GameObject.transform.position = startPosition;
            bullet.GameObject.transform.DOMove(target, moveDuration);

            
            Rotate(bullet);
        }

        private void Rotate(IBullet bullet)
        {
            Vector3 targetRotation = new Vector3(104, bullet.GameObject.transform.localEulerAngles.y, bullet.GameObject.transform.localEulerAngles.z);
            
            bullet.GameObject.transform
                .DOLocalRotate(targetRotation, 0f).SetEase(Ease.Linear);
        }
    }
}