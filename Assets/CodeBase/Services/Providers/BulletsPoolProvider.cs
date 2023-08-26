using System.Collections.Generic;
using CodeBase.Enums;
using CodeBase.Services.ObjectPool;

namespace CodeBase.Services.Providers
{
    public class BulletsPoolProvider : IProvider<Dictionary<WeaponTypeId, GameObjectPool>>
    {
        private Dictionary<WeaponTypeId, GameObjectPool> _bulletsPool = new();
        
        public Dictionary<WeaponTypeId, GameObjectPool> Get() => 
            _bulletsPool;

        public void Set(Dictionary<WeaponTypeId, GameObjectPool> bulletsPool) => 
            _bulletsPool = bulletsPool;
    }
}