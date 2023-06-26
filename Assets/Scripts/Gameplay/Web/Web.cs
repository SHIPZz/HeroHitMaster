using Enums;
using Gameplay.Character;
using ScriptableObjects.WebSettings;
using Services.ObjectPool;
using UnityEngine;
using Zenject;
using static DG.Tweening.DOVirtual;

namespace Gameplay.Web
{
    public class Web : MonoBehaviour, IWeapon
    {
        [SerializeField] private WebTypeId _webTypeId;
        
        private WebSettings _webSettings;
        private Transform _rightHand;
        
        private WebMovement _webMovement;

        [Inject]
        private void Construct([Inject(Id = WebTypeId.SpiderWeb)] WebSettings webSettings, WebMovement webMovement)
        {
            _webMovement = webMovement;
            _webSettings = webSettings;
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.TryGetComponent(out IDamageable damageable))
            {
                print(other.gameObject.name);
                // damageable.TakeDamage(_webSettings.Damage);
            }
        }

        public void Shoot(Vector3 target, Vector3 initialPosition)
        {
            _webMovement.Move(target, this, initialPosition);
        }
    }
}