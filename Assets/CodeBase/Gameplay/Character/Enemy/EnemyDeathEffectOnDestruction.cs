using System;
using CodeBase.Gameplay.MaterialChanger;
using CodeBase.Services.Data;
using UnityEngine;
using Zenject;

namespace CodeBase.Gameplay.Character.Enemy
{
    public class EnemyDeathEffectOnDestruction : IInitializable, IDisposable
    {
        private ParticleSystem _dieEffect;
        private readonly EnemyEffectDataStorage _enemyEffectDataStorage;
        private bool _canPlayEffect = true;
        private readonly Enemy _enemy;

        public EnemyDeathEffectOnDestruction(EnemyEffectDataStorage enemyEffectDataStorage, Enemy enemy)
        {
            _enemy = enemy;
            _enemyEffectDataStorage = enemyEffectDataStorage;
        }
        
        public void Initialize()
        {
            _enemy.QuickDestroyed += Play;
            _enemy.GetComponent<DieOnAnimationEvent>().Dead += Play;
            _enemy.GetComponent<SkinnedMaterialChanger>().Changed += BlockEffect;
        }

        public void Dispose()
        {
            _enemy.QuickDestroyed -= Play;
            _enemy.GetComponent<DieOnAnimationEvent>().Dead -= Play;
            _enemy.GetComponent<SkinnedMaterialChanger>().Changed -= BlockEffect;
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