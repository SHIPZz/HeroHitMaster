using DG.Tweening;
using UnityEngine;

namespace CodeBase.Gameplay.Bullet.Web
{
    public class WebMovement : IBulletMovement
    {
        private const float Duration = 0.3f;
        
        public void Move(Vector3 target, IBullet bullet, Vector3 startPosition, Rigidbody rigidbody)
        {
            bullet.GameObject.transform.position = startPosition;
            bullet.GameObject.transform.DOMove(target, Duration);
        }
    }
}