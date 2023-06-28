using DG.Tweening;
using UnityEngine;

namespace Gameplay.Bullet
{
    public class DefaultBulletMovement : IBulletMovement
    {
        public void Move(Vector3 target, IBullet bullet, Vector3 startPosition, float duration)
        {
            bullet.GameObject.transform.position = startPosition;
            bullet.GameObject.transform.DOMove(target, duration);
        }
    }
}