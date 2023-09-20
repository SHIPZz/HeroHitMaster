using System.Collections.Generic;
using CodeBase.Enums;
using UnityEngine;

namespace CodeBase.ScriptableObjects
{
    [CreateAssetMenu(fileName = "EffectData", menuName = "Gameplay/EffectData")]
    public class EffectData : ScriptableObject
    {
        public ParticleTypeId ParticleTypeId;
        public ParticleSystem ParticleSystem;
        public List<ParticleSystem> ParticleSystems;
    }
}