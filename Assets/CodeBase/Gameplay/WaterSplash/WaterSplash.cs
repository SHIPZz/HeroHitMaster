using System.Collections.Generic;
using CodeBase.Enums;
using CodeBase.Gameplay.Collision;
using CodeBase.Services.ObjectPool;
using CodeBase.Services.Providers;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace CodeBase.Gameplay.WaterSplash
{
    public class WaterSplash : MonoBehaviour
    {
        [SerializeField] private TriggerObserver _triggerObserver;
        
        private Dictionary<ParticleTypeId, GameObjectPool> _effectsPool;
        private Dictionary<SoundTypeId, GameObjectPool> _soundsPool;

        [Inject]
        private void Construct(IProvider<EffectsPoolProvider> provider)
        {
            _effectsPool = provider.Get().EffectsPool;
            _soundsPool = provider.Get().SoundPools;
        }
        
        private void OnEnable() =>
            _triggerObserver.Entered += OnTriggerEntered;

        private void OnDisable() =>
            _triggerObserver.Entered -= OnTriggerEntered;

        private void OnTriggerEntered(Collider other)
        {
            var effect = _effectsPool[ParticleTypeId.WaterSplash].Pop().GetComponent<ParticleSystem>();
            var sound = _soundsPool[SoundTypeId.WaterSplash].Pop().GetComponent<AudioSource>();
            
            effect.transform.localPosition = other.transform.position;
            effect.Play();
            sound.Play();

            DOTween.Sequence().AppendInterval(3f).OnComplete(() =>
            {
                _effectsPool[ParticleTypeId.WaterSplash].Push(effect.gameObject);
                _soundsPool[SoundTypeId.WaterSplash].Push(sound.gameObject);
            });
        }
    }
}