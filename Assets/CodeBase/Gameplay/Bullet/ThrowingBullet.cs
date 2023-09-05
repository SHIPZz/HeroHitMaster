using UnityEngine;

namespace CodeBase.Gameplay.Bullet
{
    public class ThrowingBullet : Bullet
    {
        [field: SerializeField] public Transform BulletModel { get; private set; }
        [field: SerializeField] public Transform Rotator { get; private set; }
    }
}