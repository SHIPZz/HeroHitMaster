using UnityEngine;

namespace CodeBase.Gameplay.Bullet
{
    public interface IBulletMovement
    {
        void Move(Vector3 target, Bullet bullet, Vector3 startPosition);
    }
}