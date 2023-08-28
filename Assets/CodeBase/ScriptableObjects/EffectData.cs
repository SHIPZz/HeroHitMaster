using CodeBase.Enums;
using UnityEngine;

namespace CodeBase.Services.Storages.Effect
{
    [CreateAssetMenu(fileName = "EffectData", menuName = "Gameplay/EffectData")]
    public class EffectData : ScriptableObject
    {
        public ParticleTypeId ParticleTypeId;
        public ParticleSystem ParticleSystem;
    }
}