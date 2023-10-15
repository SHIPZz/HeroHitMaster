using CodeBase.Enums;
using CodeBase.Gameplay.ObjectBodyPart;
using CodeBase.Services.Storages.Sound;
using UnityEngine;
using Zenject;

namespace CodeBase.Gameplay.EffectsData
{
    public class PlayDestructionSoundOnHit : MonoBehaviour
    {
        [SerializeField] private DestroyableObject _destroyableObject;
        [SerializeField] private SoundTypeId _soundTypeId;
        
         private AudioSource _audioSource;

        [Inject]
        private void Construct(ISoundStorage soundStorage) => 
            _audioSource = soundStorage.Get(_soundTypeId);

        private void OnEnable() => 
            _destroyableObject.Destroyed += Play;

        private void OnDisable() => 
            _destroyableObject.Destroyed -= Play;

        private void Play(DestroyableObjectTypeId obj) =>
            _audioSource.Play();
    }
}