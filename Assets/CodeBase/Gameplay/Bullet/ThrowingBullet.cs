using CodeBase.Gameplay.ObjectBodyPart;
using UnityEngine;

namespace CodeBase.Gameplay.Bullet
{
    public class ThrowingBullet : Bullet
    {
        [field: SerializeField] public Transform BulletModel { get; private set; }

        protected override void DoDamage(Collider other)
        {
            if (other.TryGetComponent(out IDestroyable destroyable))
                destroyable.Destroy();

            if (other.TryGetComponent(out EnemyPartForKnifeHolder enemyPartForKnifeHolder))
            {
                transform.position = other.transform.position;
                transform.SetParent(enemyPartForKnifeHolder.transform);
            }
        }
    }
}