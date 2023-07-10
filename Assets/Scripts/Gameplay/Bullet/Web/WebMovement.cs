using DG.Tweening;
using Gameplay.Bullet;
using UnityEngine;
using Zenject;

namespace Gameplay.Web
{
    public class WebMovement : IBulletMovement
    {
        public void Move(Vector3 target, IBullet bullet, Vector3 startPosition, float duration, Rigidbody rigidbody)
        {
            bullet.GameObject.transform.position = startPosition;
            bullet.GameObject.transform.DOMove(target, duration);
            // rigidbody.DOMove(target, duration);
        }
    }
}