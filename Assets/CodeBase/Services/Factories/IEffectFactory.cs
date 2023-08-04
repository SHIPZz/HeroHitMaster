using CodeBase.Enums;
using UnityEngine;

namespace CodeBase.Services.Factories
{
    public interface IEffectFactory
    {
        AudioSource Create(SoundTypeId soundTypeId);
        AudioSource Create(AudioSource audioSource);
        ParticleSystem Create(ParticleSystem particleSystem);
    }
}