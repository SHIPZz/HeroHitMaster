using System;
using CodeBase.Enums;
using CodeBase.Services.Storages.Sound;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace CodeBase.Gameplay.Collision
{
    public class PlaySoundOnMoveTrigger : MonoBehaviour
    {
        [SerializeField] private SoundTypeId _soundTypeId;
        [SerializeField] private BoatMoveOnPlayerTrigger boatMoveOnPlayerTrigger;

        private AudioSource _targetSound;

        [Inject]
        private void Construct(ISoundStorage soundStorage)
        {
            _targetSound = soundStorage.Get(_soundTypeId);
        }

        private void OnEnable()
        {
            boatMoveOnPlayerTrigger.MovementStarted += Play;
            boatMoveOnPlayerTrigger.MovementCompleted += Stop;
        }

        private void OnDisable()
        {
            boatMoveOnPlayerTrigger.MovementStarted -= Play;
            boatMoveOnPlayerTrigger.MovementCompleted -= Stop;
        }

        private void Stop()
        {
            _targetSound.DOFade(0, 1f);
        }

        private void Play() => 
        _targetSound.Play();
    }
}