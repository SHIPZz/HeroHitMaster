using CodeBase.Enums;
using CodeBase.Gameplay.ObjectBodyPart;
using CodeBase.Services.Storages.Sound;
using UnityEngine;
using Zenject;

namespace CodeBase.Gameplay.Collision
{
    public class PlayGlassDestructionSoundOnHit : MonoBehaviour
    {
        [SerializeField] private DestroyableObject _destroyableObject;
        
         private AudioSource _audioSource;

        [Inject]
        private void Construct(ISoundStorage soundStorage)
        {
            _audioSource = soundStorage.Get(SoundTypeId.GlassDestruction);
        }
        
        private void OnEnable() => 
            _destroyableObject.Destroyed += Play;

        private void OnDisable() => 
            _destroyableObject.Destroyed -= Play;

        private void Play(DestroyableObjectTypeId obj) =>
            _audioSource.Play();
    }
}