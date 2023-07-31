using CodeBase.Enums;
using UnityEngine;

namespace CodeBase.Services.Factories
{
    public interface ISoundFactory
    {
        AudioSource Create(SoundTypeId soundTypeId);
    }
}