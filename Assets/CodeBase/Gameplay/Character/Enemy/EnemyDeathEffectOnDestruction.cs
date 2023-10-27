using System;
using CodeBase.Gameplay.MaterialChanger;
using CodeBase.Services.Data;
using UnityEngine;
using Zenject;

namespace CodeBase.Gameplay.Character.Enemy
{
    public class EnemyDeathEffectOnDestruction : IInitializable, IDisposable
    {
        private readonly IMaterialChanger _materialChanger;
        private readonly EnemyEffectDataStorage _enemyEffectDataStorage;
        private readonly Enemy _enemy;
        private ParticleSystem _dieEffect;
        private bool _canPlayEffect = true;

        public EnemyDeathEffectOnDestruction(EnemyEffectDataStorage enemyEffectDataStorage, Enemy enemy, IMaterialChanger materialChanger)
        {
            _materialChanger = materialChanger;
            _enemy = enemy;
            _enemyEffectDataStorage = enemyEffectDataStorage;
        }
        
        public void Initialize()
        {
            _enemy.QuickDestroyed += Play;
            _enemy.GetComponent<DieOnAnimationEvent>().Dead += Play;
            _materialChanger.StartedChanged += BlockEffect;
        }

        public void Dispose()
        {
            _enemy.QuickDestroyed -= Play;
            _enemy.GetComponent<DieOnAnimationEvent>().Dead -= Play;
            _materialChanger.StartedChanged -= BlockEffect;
        }

        private void BlockEffect() =>
            _canPlayEffect = false;

        private void Play(Enemy enemy)
        {
            if (!_canPlayEffect)
                return;

            _dieEffect = _enemyEffectDataStorage.GetDeathEnemyEffect(enemy.EnemyTypeId);
            _dieEffect.transform.position = enemy.transform.position + Vector3.up;
            _dieEffect.Play();
        }
    }
}