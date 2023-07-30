using System.Collections.Generic;
using CodeBase.Gameplay.Character.Enemy;
using CodeBase.Services.Factories;
using Enums;

namespace CodeBase.Services.Storages
{
    public class EnemyStorage : IEnemyStorage
    {
        private readonly Dictionary<EnemyTypeId, Enemy> _enemies = new();
        private readonly EnemyFactory _enemyFactory;

        public EnemyStorage(EnemySetting enemySetting, EnemyFactory enemyFactory)
        {
            _enemyFactory = enemyFactory;
            FillDictionary(enemySetting.EnemyTypeIds);
        }

        public Enemy Get(EnemyTypeId enemyTypeId) =>
            _enemies[enemyTypeId];

        public List<Enemy> GetAll()
        {
            var enemies = new List<Enemy>();

            foreach (Enemy enemy in _enemies.Values)
            {
                enemies.Add(enemy);
            }

            return enemies;
        }

        private void FillDictionary(List<EnemyTypeId> enemySettingEnemyTypeIds)
        {
            foreach (var enemyTypeId in enemySettingEnemyTypeIds)
            {
                Enemy enemy = _enemyFactory.CreateBy(enemyTypeId);
                _enemies[enemyTypeId] = enemy;
            }
        }
    }
}