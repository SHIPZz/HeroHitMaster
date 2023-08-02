using System.Collections.Generic;
using CodeBase.Services.Data;
using CodeBase.Services.Storages;
using DG.Tweening;
using UnityEngine;
using Random = System.Random;

namespace CodeBase.Gameplay.Bullet
{
    public class KnifeMovement : IBulletMovement
    {
        private const float RotateDuration = 0.3f;
        private const float MoveDuration = 0.5f;
        private static readonly Random _random = new();

        private readonly List<Vector3> _rotationVectors = new()
        {
            new Vector3(0, 0, 360),
            new Vector3(0, 360, 0),
            new Vector3(360, 0, 0)
        };

        private readonly BulletStaticDataService _bulletStaticDataService;

        public KnifeMovement(BulletStaticDataService bulletStaticDataService)
        {
            _bulletStaticDataService = bulletStaticDataService;
        }

        public void Move(Vector3 target, IBullet bullet, Vector3 startPosition, Rigidbody rigidbody)
        {
            var rotateDuration = _bulletStaticDataService.GetBy(bullet.BulletTypeId).RotateDuration;
            var moveDuration = _bulletStaticDataService.GetBy(bullet.BulletTypeId).MovementDuration;
            
            bullet.GameObject.transform.position = startPosition;
            bullet.GameObject.transform.DOMove(target, moveDuration);

            Vector3 direction = target - startPosition;
            bullet.GameObject.transform.up = direction;
            
            Rotate(bullet, rotateDuration);
        }

        private void Rotate(IBullet bullet, float rotateDuration)
        {
            int randomValue = _random.Next(0, _rotationVectors.Count - 1);
            Vector3 randomVector = _rotationVectors[randomValue];

            bullet.GameObject.transform.DOLocalRotate(randomVector, rotateDuration).SetRelative(true).SetEase(Ease.Linear);
        }
    }
}