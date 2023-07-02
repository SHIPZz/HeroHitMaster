using System.Collections.Generic;
using System.Linq;
using Constants;
using Enums;
using Gameplay.Character;
using ScriptableObjects.WebSettings;
using UnityEngine;
using Zenject;

namespace Gameplay.Bullet
{
    public class Bullet : MonoBehaviour, IBullet
    {
        [field: SerializeField] public BulletTypeId BulletTypeId { get; private set; }
        
        private BulletSettings _bulletSetting;
        private Transform _rightHand;

        [Inject]
        private void Construct(List<BulletSettings> bulletSettingsList)
        {
            _bulletSetting = bulletSettingsList.Find(x => x.BulletTypeId == BulletTypeId);
        }

        public GameObject GameObject => 
            gameObject;

        private void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.TryGetComponent(out IDamageable damageable))
            {
                damageable.TakeDamage(_bulletSetting.Damage);
            }
        }

    }
}