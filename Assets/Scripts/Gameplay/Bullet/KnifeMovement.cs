using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Random = System.Random;

namespace Gameplay.Bullet
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

        public void Move(Vector3 target, IBullet bullet, Vector3 startPosition, Rigidbody rigidbody)
        {
            bullet.GameObject.transform.position = startPosition;
            bullet.GameObject.transform.DOMove(target, MoveDuration);

            Vector3 direction = target - startPosition;
            bullet.BulletModel.up = direction;

            int randomValue = _random.Next(0, _rotationVectors.Count - 1);
            Vector3 randomVector = _rotationVectors[randomValue];

            bullet.BulletModel.DOLocalRotate(randomVector, RotateDuration).SetRelative(true).SetEase(Ease.Linear);
        }
    }
}