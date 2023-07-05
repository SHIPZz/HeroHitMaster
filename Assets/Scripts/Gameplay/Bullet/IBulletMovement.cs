using UnityEngine;

namespace Gameplay.Bullet
{
    public interface IBulletMovement
    {
        void Move(Vector3 target, IBullet bullet, Vector3 startPosition, float duration, Rigidbody rigidbody);
    }
}