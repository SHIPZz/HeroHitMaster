using DG.Tweening;
using UnityEngine;

namespace CodeBase.Gameplay.Bullet
{
    public class DefaultBulletMovement : IBulletMovement
    {
        public void Move(Vector3 target, IBullet bullet, Vector3 startPosition, Rigidbody rigidbody)
        {
            bullet.GameObject.transform.position = startPosition;
            bullet.GameObject.transform.DOMove(target, 0.3f);
        }
    }
}