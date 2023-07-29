using System.Collections.Generic;
using Enums;
using Gameplay.Character.Enemy;

namespace CodeBase.Services.Storages
{
    public interface IEnemyStorage
    {
        Enemy Get(EnemyTypeId enemyTypeId);
        List<Enemy> GetAll();
    }
}