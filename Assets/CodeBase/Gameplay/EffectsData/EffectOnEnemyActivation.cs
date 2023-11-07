using System.Collections.Generic;
using CodeBase.Enums;
using CodeBase.Gameplay.Character.Enemy;
using CodeBase.Services.Data;
using CodeBase.Services.Factories;
using CodeBase.Services.Storages.Sound;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace CodeBase.Gameplay.EffectsData
{
    public class EffectOnEnemyActivation : MonoBehaviour
    {
        [SerializeField] private Vector3 _offset;

        private List<ParticleSystem> _effects;
        private ActivateEnemiesOnPlayerEnter _activateEnemy;
        private AudioSource _targetSound;
        private IEffectFactory _effectFactory;

        [Inject]
        private void Construct(ISoundStorage soundStorage, EffectStaticDataService effectStaticDataService, IEffectFactory effectFactory)
        {
            _effectFactory = effectFactory;
            _effects = effectStaticDataService.Get(ParticleTypeId.AppearEnemyEmojis).ParticleSystems;
            _targetSound = soundStorage.Get(SoundTypeId.AppearEnemy);
        }

        private void Awake() =>
            _activateEnemy = GetComponent<ActivateEnemiesOnPlayerEnter>();

        private void OnEnable() =>
            _activateEnemy.Activated += Play;

        private void OnDisable() =>
            _activateEnemy.Activated -= Play;

        private void Play(Enemy enemy)
        {
            var randomId = Random.Range(0, _effects.Count - 1);
            ParticleSystem randomEffectPrefab = _effects[randomId];
            ParticleSystem effect = _effectFactory.Create(randomEffectPrefab);
            effect.gameObject.SetActive(true);
            effect.gameObject.transform.position = enemy.transform.position + _offset;
            
            effect.Play();
            _targetSound.Play();
        }
    }
}