using System;
using CodeBase.Gameplay.Character.Players.Shoot;
using Services.Providers;
using Zenject;

namespace CodeBase.Gameplay.EffectPlaying
{
    public class EffectOnShoot : IInitializable, IDisposable
    {
        private readonly SoundProvider _soundProvider;
        private readonly ShootingOnAnimationEvent _shootingOnAnimationEvent;

        public EffectOnShoot(SoundProvider soundProvider, ShootingOnAnimationEvent shootingOnAnimationEvent)
        {
            _shootingOnAnimationEvent = shootingOnAnimationEvent;
            _soundProvider = soundProvider;
        }

        public void Initialize() => 
            _shootingOnAnimationEvent.Shooted += PlayEffects;

        public void Dispose() => 
            _shootingOnAnimationEvent.Shooted -= PlayEffects;

        private void PlayEffects()
        {
            _soundProvider.ShootSound.Play();
        }
    }
}