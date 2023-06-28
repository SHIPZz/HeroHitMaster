using Constants;
using Enums;
using Gameplay.Character;
using ScriptableObjects.WebSettings;
using UnityEngine;
using Zenject;

namespace Gameplay.Web
{
    public class Bullet : MonoBehaviour, IBullet
    {
        [field: SerializeField] public WebTypeId WebTypeId { get; private set; }
        
        private WebSettings _webSettings;
        private Transform _rightHand;

        public GameObject GameObject => 
            gameObject;

        public int Id { get; } = ShootableObjectId.SpiderWeb;

        private void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.TryGetComponent(out IDamageable damageable))
            {
                print("damageable");
                // damageable.TakeDamage(_webSettings.Damage);
            }
        }

    }
}