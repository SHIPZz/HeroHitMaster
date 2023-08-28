using CodeBase.Enums;
using UnityEngine;

namespace CodeBase.Services.Storages.Effect
{
    public interface IEffectStorage
    {
        ParticleSystem Get(ParticleTypeId particleTypeId);
    }
}