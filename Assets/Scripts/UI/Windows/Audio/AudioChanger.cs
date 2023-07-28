using System.Collections.Generic;
using Services.Storages;
using UnityEngine;

namespace Windows.Audio
{
    public class AudioChanger
    {
        private readonly SoundStorage _soundStorage;

        public AudioChanger(SoundStorage soundStorage)
        {
            _soundStorage = soundStorage;
        }

        public void Change(float value)
        {
            List<AudioSource> allSounds = _soundStorage.Get();
            allSounds.ForEach(x => x.volume = value);
        }
    }
}