using System.Collections.Generic;
using System.Threading.Tasks;
using CodeBase.Enums;
using CodeBase.Gameplay.Character.Enemy;
using Cysharp.Threading.Tasks;

namespace CodeBase.Services.Storages.Character
{
    public interface IEnemyStorage
    {
        Enemy Get(EnemyTypeId enemyTypeId);
        List<Enemy> GetAll();
        UniTask InitTask { get; }
    }
}