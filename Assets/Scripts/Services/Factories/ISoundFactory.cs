using Enums;
using UnityEngine;

namespace Services.Factories
{
    public interface ISoundFactory
    {
        AudioSource Create(SoundTypeId soundTypeId);
    }
}