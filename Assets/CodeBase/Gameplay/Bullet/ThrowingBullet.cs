using UnityEngine;

namespace CodeBase.Gameplay.Bullet
{
    public class ThrowingBullet : Bullet
    {
        [field: SerializeField] public Transform BulletModel { get; private set; }
    }
}