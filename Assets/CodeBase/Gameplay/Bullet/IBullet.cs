using UnityEngine;

namespace Gameplay.Bullet
{
    public interface IBullet
    {
        GameObject GameObject { get; }
        Rigidbody Rigidbody { get; }
    }
}