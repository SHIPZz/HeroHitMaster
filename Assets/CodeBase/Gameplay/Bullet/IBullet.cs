using UnityEngine;

namespace CodeBase.Gameplay.Bullet
{
    public interface IBullet
    {
        GameObject GameObject { get; }
        Rigidbody Rigidbody { get; }

        void DoDamage(Collider other);
    }
}