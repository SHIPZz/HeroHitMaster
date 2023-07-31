using System.Collections.Generic;
using CodeBase.Enums;
using CodeBase.Gameplay.Character.Enemy;

namespace CodeBase.Services.Storages
{
    public interface IEnemyStorage
    {
        Enemy Get(EnemyTypeId enemyTypeId);
        List<Enemy> GetAll();
    }
}