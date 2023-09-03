using System;
using CodeBase.Enums;
using CodeBase.Gameplay.Collision;
using CodeBase.Services.Storages.Sound;
using UnityEngine;
using Zenject;

namespace CodeBase.Gameplay.ExplosionBarrel
{
    public class ExplosionBarrel : MonoBehaviour
    {
        [SerializeField] private TriggerObserver _triggerObserver;
        [SerializeField] private ParticleSystem _explosionEffect;
        
        private AudioSource _explosionSound;

        public event Action Exploded;

        [Inject]
        private void Construct(ISoundStorage soundStorage)
        {
            _explosionSound = soundStorage.Get(SoundTypeId.BarrelExplosion);
        }

        private void OnEnable()
        {
            _triggerObserver.CollisionEntered += OnCollisionEntered;
        }

        private void OnDisable()
        {
            _triggerObserver.CollisionEntered -= OnCollisionEntered;
        }

        private void OnCollisionEntered(UnityEngine.Collision collision)
        {
            if (collision.gameObject.TryGetComponent(out Bullet.Bullet bullet))
            {
                _explosionEffect.Play();
                _explosionSound.Play();
                Exploded?.Invoke();
                // gameObject.SetActive(false);
            }
        }
    }
}