using System.Collections.Generic;
using CodeBase.Enums;
using CodeBase.Services.ObjectPool;

namespace CodeBase.Services.Providers
{
    public class BulletsPoolProvider : IProvider<Dictionary<BulletTypeId, GameObjectPool>>
    {
        private Dictionary<BulletTypeId, GameObjectPool> _bulletsPool = new();
        
        public Dictionary<BulletTypeId, GameObjectPool> Get() => 
            _bulletsPool;

        public void Set(Dictionary<BulletTypeId, GameObjectPool> bulletsPool) => 
            _bulletsPool = bulletsPool;
    }
}