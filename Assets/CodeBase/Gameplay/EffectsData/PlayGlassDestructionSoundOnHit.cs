using CodeBase.Enums;
using CodeBase.Gameplay.ObjectBodyPart;
using CodeBase.Services.Storages.Sound;
using UnityEngine;
using Zenject;

namespace CodeBase.Gameplay.EffectsData
{
    public class PlayGlassDestructionSoundOnHit : MonoBehaviour
    {
        [SerializeField] private DestroyableObject destroyableObject;
        
         private AudioSource _audioSource;

        [Inject]
        private void Construct(ISoundStorage soundStorage)
        {
            _audioSource = soundStorage.Get(SoundTypeId.GlassDestruction);
        }
        
        private void OnEnable() => 
            destroyableObject.Destroyed += Play;

        private void OnDisable() => 
            destroyableObject.Destroyed -= Play;

        private void Play(DestroyableObjectTypeId obj) =>
            _audioSource.Play();
    }
}