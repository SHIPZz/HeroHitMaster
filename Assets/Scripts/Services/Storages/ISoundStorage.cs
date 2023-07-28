using Enums;
using UnityEngine;

namespace Services.Storages
{
    public interface ISoundStorage
    {
        AudioSource Get(SoundTypeId soundTypeId);
    }
}