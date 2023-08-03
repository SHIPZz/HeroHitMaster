using System;
using System.Collections.Generic;
using CodeBase.Gameplay.Character.Enemy;
using CodeBase.Gameplay.MaterialChanger;
using CodeBase.Services.Data;
using CodeBase.Services.Storages.Character;
using UnityEngine;
using Zenject;

namespace CodeBase.Gameplay.EffectPlaying
{
    public class EnemiesDeathEffectOnDestruction : IInitializable
    {
        private ParticleSystem _dieEffect;
        private readonly List<Enemy> _enemies;
        private readonly EnemyEffectDataStorage _enemyEffectDataStorage;
        private bool _canPlayEffect = true;

        public EnemiesDeathEffectOnDestruction(IEnemyStorage enemyStorage,
            EnemyEffectDataStorage enemyEffectDataStorage)
        {
            _enemyEffectDataStorage = enemyEffectDataStorage;
            _enemies = enemyStorage.GetAll();
        }

        public void Initialize()
        {
            _enemies.ForEach(x =>
            {
                x.QuickDestroyed += Play;
                x.GetComponent<DieOnAnimationEvent>().Dead += Play;
                x.GetComponent<SkinnedMaterialChanger>().Changed += BlockEffect;
            });
        }

        private void BlockEffect() => 
            _canPlayEffect = false;

        private void Play(Enemy enemy)
        {
            if(!_canPlayEffect)
                return;
            
            _dieEffect = _enemyEffectDataStorage.GetDeathEnemyEffect(enemy.EnemyTypeId);
            _dieEffect.transform.position = enemy.transform.position + Vector3.up;
            _dieEffect.Play();
        }
    }
}