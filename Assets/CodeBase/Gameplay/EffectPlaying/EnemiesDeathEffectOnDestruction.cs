using System.Collections.Generic;
using CodeBase.Gameplay.Character.Enemy;
using CodeBase.Gameplay.MaterialChanger;
using CodeBase.Services.Data;
using UnityEngine;

namespace CodeBase.Gameplay.EffectPlaying
{
    public class EnemiesDeathEffectOnDestruction
    {
        private ParticleSystem _dieEffect;
        private readonly EnemyEffectDataStorage _enemyEffectDataStorage;
        private bool _canPlayEffect = true;

        public EnemiesDeathEffectOnDestruction(EnemyEffectDataStorage enemyEffectDataStorage) => 
            _enemyEffectDataStorage = enemyEffectDataStorage;

        public void Init(List<Enemy> enemies)
        {
            enemies.ForEach(x =>
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
            if (!_canPlayEffect)
                return;

            _dieEffect = _enemyEffectDataStorage.GetDeathEnemyEffect(enemy.EnemyTypeId);
            _dieEffect.transform.position = enemy.transform.position + Vector3.up;
            _dieEffect.Play();
        }
    }
}