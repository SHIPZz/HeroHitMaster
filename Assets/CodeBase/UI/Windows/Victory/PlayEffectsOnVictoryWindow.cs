using System.Collections.Generic;
using CodeBase.Enums;
using CodeBase.Services.Providers;
using UnityEngine;
using Zenject;

namespace CodeBase.UI.Windows.Victory
{
    public class PlayEffectsOnVictoryWindow : MonoBehaviour
    {
        [SerializeField] private List<ParticleSystem> _particleSystems;
        
        private Window _victoryWindow;

        [Inject]
        private void Construct(WindowProvider windowProvider)
        {
            _victoryWindow = windowProvider.Windows[WindowTypeId.Victory];
            _victoryWindow.Opened += PlayEffects;
        }

        private void OnDisable() => 
            _victoryWindow.Opened -= PlayEffects;

        private void PlayEffects()
        {
            foreach (ParticleSystem particleSystem in _particleSystems)
                particleSystem.Play();
        }
    }
}