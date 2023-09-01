using System.Collections.Generic;
using CodeBase.Enums;
using CodeBase.Services.ObjectPool;

namespace CodeBase.Services.Providers
{
    public class EffectsPoolProvider : IProvider<EffectsPoolProvider>
    {
        public Dictionary<ParticleTypeId, GameObjectPool> EffectsPool = new();
        public Dictionary<SoundTypeId, GameObjectPool> SoundPools = new();

        public EffectsPoolProvider Get() =>
            this;

        public void Set(EffectsPoolProvider t)
        {
            
        }
    }
}