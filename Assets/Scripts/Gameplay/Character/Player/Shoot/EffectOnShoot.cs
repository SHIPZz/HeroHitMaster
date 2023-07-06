using UnityEngine;

namespace Gameplay.Character.Player.Shoot
{
    public class EffectOnShoot
    {
        private readonly AudioSource _audioSource;

        public EffectOnShoot(AudioSource audioSource)
        {
            _audioSource = audioSource;
            _audioSource.playOnAwake = false;
        }

        public void PlayEffects()
        {
            _audioSource.Play();
        }
    }
}