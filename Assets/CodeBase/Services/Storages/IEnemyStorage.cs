using System.Collections.Generic;
using CodeBase.Gameplay.Character.Enemy;
using Enums;

namespace CodeBase.Services.Storages
{
    public interface IEnemyStorage
    {
        Enemy Get(EnemyTypeId enemyTypeId);
        List<Enemy> GetAll();
    }
}