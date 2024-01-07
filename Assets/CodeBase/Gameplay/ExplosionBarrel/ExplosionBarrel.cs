using System;
using CodeBase.Enums;
using CodeBase.Gameplay.Collision;
using CodeBase.Services.Storages.Effect;
using CodeBase.Services.Storages.Sound;
using UnityEngine;
using Zenject;

namespace CodeBase.Gameplay.ExplosionBarrel
{
    [RequireComponent(typeof(KillAllEnemiesOnExlposionBarrel))]
    public class ExplosionBarrel : MonoBehaviour
    {
        [SerializeField] private TriggerObserver _triggerObserver;

        private ParticleSystem _explosionEffect;
        private AudioSource _explosionSound;
        private KillAllEnemiesOnExlposionBarrel _killAllEnemiesOnExlposion;

        public event Action Exploded;
        public event Action StartedExplode;

        [Inject]
        private void Construct(ISoundStorage soundStorage, IEffectStorage effectStorage)
        {
            _explosionEffect = effectStorage.Get(ParticleTypeId.BarrelExplosion);
            _explosionSound = soundStorage.Get(SoundTypeId.BarrelExplosion);
        }

        private void Awake() => 
            _killAllEnemiesOnExlposion = GetComponent<KillAllEnemiesOnExlposionBarrel>();

        private void OnEnable() =>
            _triggerObserver.CollisionEntered += OnKnifeEntered;

        private void OnDisable() =>
            _triggerObserver.CollisionEntered -= OnKnifeEntered;

        private void OnKnifeEntered(UnityEngine.Collision knife)
        {
            if (_killAllEnemiesOnExlposion.HasInactiveEnemies)
                return;

            knife.gameObject.SetActive(false);
            StartedExplode?.Invoke();
            _explosionEffect.transform.position = transform.position;
            _explosionEffect.Play();
            _explosionSound.Play();
            Exploded?.Invoke();
            gameObject.SetActive(false);
        }
    }
}