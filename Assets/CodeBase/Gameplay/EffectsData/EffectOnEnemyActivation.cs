using System.Collections.Generic;
using CodeBase.Enums;
using CodeBase.Services.Storages.Sound;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace CodeBase.Gameplay.Character.Enemy
{
    public class EffectOnEnemyActivation : MonoBehaviour
    {
        [SerializeField] private List<ParticleSystem> _effects;
        [SerializeField] private Vector3 _offset;
        
        private ActivateEnemiesOnPlayerEnter _activateEnemy;
        private AudioSource _targetSound;

        [Inject]
        private void Construct(ISoundStorage soundStorage) => 
            _targetSound = soundStorage.Get(SoundTypeId.AppearEnemy);

        private void Awake() => 
            _activateEnemy = GetComponent<ActivateEnemiesOnPlayerEnter>();

        private void OnEnable() => 
            _activateEnemy.Activated += Play;

        private void OnDisable() => 
            _activateEnemy.Activated -= Play;

        private void Play(Enemy enemy)
        {
            print("effect play");
            var randomId = Random.Range(0, _effects.Count - 1);
            ParticleSystem randomEffect = _effects[randomId];
            randomEffect.gameObject.transform.position = enemy.transform.position + _offset;
            randomEffect.Play();
            _targetSound.Play();
        }
    }
}