using UnityEngine;

namespace Gameplay.Bullet
{
    public interface IBullet
    {
        GameObject GameObject { get; }
        int Id { get; }
    }
}