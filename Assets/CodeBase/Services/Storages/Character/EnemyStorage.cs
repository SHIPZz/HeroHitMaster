using System.Collections.Generic;
using CodeBase.Enums;
using CodeBase.Gameplay.Character.Enemy;
using CodeBase.Services.Factories;
using Cysharp.Threading.Tasks;

namespace CodeBase.Services.Storages.Character
{
    public class EnemyStorage : IEnemyStorage
    {
        private readonly EnemyFactory _enemyFactory;
        private Dictionary<EnemyTypeId, Enemy> _enemies;

        private readonly UniTask _initTask;

        public UniTask InitTask => _initTask;

        public EnemyStorage(EnemyFactory enemyFactory)
        {
            _enemyFactory = enemyFactory;

            _initTask = FillDictionary();
        }

        public Enemy Get(EnemyTypeId enemyTypeId)
        {
            return _enemies[enemyTypeId];
        }

        public List<Enemy> GetAll()
        {
            var enemies = new List<Enemy>(_enemies.Values);
            return enemies;
        }

        private async UniTask FillDictionary() =>
            _enemies = await _enemyFactory.CreateAll();
    }
}