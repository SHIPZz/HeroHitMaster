using System;
using CodeBase.Enums;
using CodeBase.Gameplay.Collision;
using CodeBase.Services.Factories;
using CodeBase.Services.Storages.Effect;
using CodeBase.Services.Storages.Sound;
using UnityEngine;
using Zenject;

namespace CodeBase.Gameplay.ExplosionBarrel
{
    public class ExplosionBarrel : MonoBehaviour
    {
        [SerializeField] private TriggerObserver _triggerObserver;

        private ParticleSystem _explosionEffect;
        private AudioSource _explosionSound;

        public event Action Exploded;
        public event Action StartedExplode;

        [Inject]
        private void Construct(ISoundStorage soundStorage, IEffectStorage effectStorage)
        {
            _explosionEffect = effectStorage.Get(ParticleTypeId.BarrelExplosion);
            _explosionSound = soundStorage.Get(SoundTypeId.BarrelExplosion);
        }

        private void OnEnable() =>
            _triggerObserver.CollisionEntered += OnCollisionEntered;

        private void OnDisable() =>
            _triggerObserver.CollisionEntered -= OnCollisionEntered;

        private void OnCollisionEntered(UnityEngine.Collision collision)
        {
            StartedExplode?.Invoke();
            _explosionEffect.transform.position = transform.position;
            _explosionEffect.Play();
            _explosionSound.Play();
            Exploded?.Invoke();
            gameObject.SetActive(false);
        }
    }
}