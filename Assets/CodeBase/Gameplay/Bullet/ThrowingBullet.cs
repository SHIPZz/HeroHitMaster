using UnityEngine;

namespace CodeBase.Gameplay.Bullet
{
    public class ThrowingBullet : Bullet
    {
        [field: SerializeField] public Transform BulletModel { get; private set; }

        protected override void DoDamage(Collider other)
        {
            if (other.TryGetComponent(out EnemyPartForKnifeHolder enemyPartForKnifeHolder))
            {
                print("1");
                transform.SetParent(enemyPartForKnifeHolder.transform);
            }
        }
    }
}