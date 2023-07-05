using DG.Tweening;
using UnityEngine;

namespace Gameplay.Bullet
{
    public class DefaultBulletMovement : IBulletMovement
    {
        public void Move(Vector3 target, IBullet bullet, Vector3 startPosition, float duration, Rigidbody rigidbody)
        {
            bullet.GameObject.transform.position = startPosition;
            rigidbody.transform.DOMove(target, duration);
        }
    }
}