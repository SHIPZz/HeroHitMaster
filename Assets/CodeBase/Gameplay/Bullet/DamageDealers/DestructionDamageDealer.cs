using CodeBase.Gameplay.Character;
using CodeBase.Gameplay.ObjectBodyPart;
using UnityEngine;

namespace CodeBase.Gameplay.Bullet.DamageDealers
{
    public class DestructionDamageDealer : DamageDealer
    {
        public override void DoDamage(Collider other)
        {
            if (other.TryGetComponent(out Animator animator))
                animator.enabled = false;
            
            if(other.TryGetComponent(out IDestroyable destroyable))
                destroyable.Destroy();
            
            if(other.TryGetComponent(out IDamageable damageable))
                damageable.TakeDamage(Damage);
            
            gameObject.SetActive(false);
        }
    }
}