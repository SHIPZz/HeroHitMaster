using CodeBase.Enums;
using CodeBase.Gameplay.Collision;
using CodeBase.Services.Storages.Sound;
using UnityEngine;
using Zenject;

namespace CodeBase.Gameplay.EffectsData
{
    [RequireComponent(typeof(JumpOnTriggerEntered))]
    public class PlaySoundOnPlayerJump : MonoBehaviour
    {
        private JumpOnTriggerEntered _jumpOnTriggerEntered;
        
        private AudioSource _jumpSound;

        [Inject]
        private void Construct(ISoundStorage soundStorage) => 
            _jumpSound = soundStorage.Get(SoundTypeId.PlayerJump);

        private void Awake() => 
            _jumpOnTriggerEntered = GetComponent<JumpOnTriggerEntered>();

        private void OnEnable() =>
            _jumpOnTriggerEntered.Jumped += Play;

        private void OnDisable() => 
        _jumpOnTriggerEntered.Jumped -= Play;

        private void Play() => 
            _jumpSound.Play();
    }
}