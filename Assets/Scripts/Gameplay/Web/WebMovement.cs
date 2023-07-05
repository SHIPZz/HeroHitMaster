using DG.Tweening;
using Gameplay.Bullet;
using UnityEngine;

namespace Gameplay.Web
{
    public class WebMovement : IBulletMovement
    {
        public void Move(Vector3 target, IBullet bullet, Vector3 startPosition, float duration, Rigidbody rigidbody)
        {
            bullet.GameObject.transform.position = startPosition;
            rigidbody.AddForce(target, ForceMode.Impulse);
            rigidbody.transform.DOMove(target, duration);
        }
    }
}