using UnityEngine;

namespace CodeBase.Gameplay.Bullet
{
    public interface IBulletMovement
    {
        void Move(Vector3 target, IBullet bullet, Vector3 startPosition, Rigidbody rigidbody);
    }
}