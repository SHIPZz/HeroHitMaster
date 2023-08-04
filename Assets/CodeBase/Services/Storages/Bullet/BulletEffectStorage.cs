using System.Collections.Generic;
using CodeBase.Enums;
using CodeBase.Services.Data;
using CodeBase.Services.Factories;
using UnityEngine;

namespace CodeBase.Services.Storages.Bullet
{
    public class BulletEffectStorage
    {
        private readonly Dictionary<BulletTypeId, ParticleSystem> _hitEffects = new();

        public BulletEffectStorage(BulletStaticDataService bulletStaticDataService, IEffectFactory effectFactory)
        {
            FillHitEffects(bulletStaticDataService, effectFactory);
        }

        public ParticleSystem Get(BulletTypeId bulletTypeId) =>
            !_hitEffects.TryGetValue(bulletTypeId, out var hitEffect) ? 
                null : 
                hitEffect;

        private void FillHitEffects(BulletStaticDataService bulletStaticDataService, IEffectFactory effectFactory)
        {
            foreach (var bulletData in bulletStaticDataService.GetAll())
            {
                if (bulletData.HitEffect == null)
                    continue;

                var hitEffect = effectFactory.Create(bulletData.HitEffect);
                _hitEffects[bulletData.BulletTypeId] = hitEffect;
            }
        }
    }
}