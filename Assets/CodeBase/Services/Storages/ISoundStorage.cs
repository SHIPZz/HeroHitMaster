using System.Collections.Generic;
using CodeBase.Enums;
using UnityEngine;

namespace CodeBase.Services.Storages
{
    public interface ISoundStorage
    {
        AudioSource Get(SoundTypeId soundTypeId);
        List<AudioSource> GetAll();
    }
}