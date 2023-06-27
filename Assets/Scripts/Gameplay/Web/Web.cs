using Constants;
using Enums;
using Gameplay.Character;
using ScriptableObjects.WebSettings;
using UnityEngine;
using Zenject;

namespace Gameplay.Web
{
    public class Web : MonoBehaviour, IShootable
    {
        [field: SerializeField] public WebTypeId WebTypeId { get; private set; }
        
        private WebSettings _webSettings;
        private Transform _rightHand;

        public int Id { get; } = ShootableObjectId.SpiderWeb;
        //
        // [Inject]
        // private void Construct([Inject(Id = WeaponTypeId.SpiderWeb)] WebSettings webSettings)
        // {
        //     _webSettings = webSettings;
        // }

        private void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.TryGetComponent(out IDamageable damageable))
            {
                print("damageable");
                // damageable.TakeDamage(_webSettings.Damage);
            }
        }

    }

    public interface IShootable
    {
        int Id { get; }
        
    }
}