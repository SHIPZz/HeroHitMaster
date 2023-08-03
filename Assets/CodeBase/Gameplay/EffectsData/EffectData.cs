using CodeBase.Enums;
using UnityEngine;

namespace CodeBase.Gameplay.EffectsData
{
    public class EffectData : MonoBehaviour
    {
        [field: SerializeField] public EffectTypeId EffectTypeId { get; private set; }
        [field: SerializeField] public ParticleSystem Effect { get; private set; }
    }
}