using CodeBase.Services.Data;
using UnityEngine;
using Zenject;

namespace CodeBase.Gameplay.Bullet.DamageDealers
{
    public abstract class DamageDealer : MonoBehaviour
    {
        protected int Damage;

        [Inject]
        private void Construct(BulletStaticDataService bulletStaticDataService) => 
            Damage = bulletStaticDataService.GetBy(GetComponent<Bullet>().BulletTypeId).Damage;

        public abstract void DoDamage(Collider other);
    }
}