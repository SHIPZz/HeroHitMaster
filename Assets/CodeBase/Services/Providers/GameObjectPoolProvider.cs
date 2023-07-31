using System.Collections.Generic;
using CodeBase.Enums;
using CodeBase.Services.ObjectPool;

namespace CodeBase.Services.Providers
{
    public class GameObjectPoolProvider 
    {
        public Dictionary<BulletTypeId, GameObjectPool> BulletPools = new();
    }
}