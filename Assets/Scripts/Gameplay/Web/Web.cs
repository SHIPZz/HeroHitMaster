using Enums;
using Gameplay.Character;
using ScriptableObjects.WebSettings;
using Services;
using UnityEngine;
using Zenject;

namespace Gameplay.Web
{
    public class Web : MonoBehaviour
    {
        [SerializeField] private WebTypeId webTypeId;
        
        private WebSettingService _webSettingService;
        private WebSettings _webSettings;
        private Transform _rightHand;

        public WebSettings WebSettings { get; private set; }

        [Inject]
        private void Construct(WebSettingService webSettingService)
        {
            _webSettingService = webSettingService;
            _webSettings = _webSettingService.Get(webTypeId);
            WebSettings = _webSettings;
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.TryGetComponent(out IDamageable damageable))
            {
                print(other.gameObject.name);
                // damageable.TakeDamage(_webSettings.Damage);
            }
        }
    }
}