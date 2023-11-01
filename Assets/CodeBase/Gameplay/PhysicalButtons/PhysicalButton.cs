using System;
using System.Collections.Generic;
using CodeBase.Enums;
using CodeBase.Gameplay.Collision;
using CodeBase.Services.Storages.Sound;
using DG.Tweening;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;
using Zenject;

namespace CodeBase.Gameplay.PhysicalButtons
{
    [RequireComponent(typeof(TriggerObserver), typeof(MeshRenderer))]
    public class PhysicalButton : SerializedMonoBehaviour
    {
        [OdinSerialize] private Dictionary<string, Material> _targetMaterials;
        [SerializeField] private float _targetPositionZ = -0.21f;
        [SerializeField] private float _increasePositionDuration = 1f;
        [SerializeField] private float _decreasePositionDuration = 0.5f;

        private TriggerObserver _triggerObserver;

        private bool _isPressing;
        private AudioSource _physicalButtonSound;
        private MeshRenderer _meshRenderer;

        public event Action Pressed;

        [Inject]
        private void Construct(ISoundStorage soundStorage) => 
            _physicalButtonSound = soundStorage.Get(SoundTypeId.PhysicalButton);

        private void Awake()
        {
            _triggerObserver = GetComponent<TriggerObserver>();
            _meshRenderer = GetComponent<MeshRenderer>();
        }

        private void OnEnable() =>
            _triggerObserver.CollisionEntered += Press;

        private void OnDisable() =>
            _triggerObserver.CollisionEntered -= Press;

        private void Press(UnityEngine.Collision obj)
        {
            if (_isPressing)
                return;

            if (!obj.gameObject.TryGetComponent(out Bullet.Bullet bullet))
                return;

            _isPressing = true;
            _physicalButtonSound.Play();
            _meshRenderer.material = _targetMaterials["Grey"];
            transform
                .DOLocalMoveZ(_targetPositionZ, _increasePositionDuration)
            .OnComplete(() => transform.DOLocalMoveZ(0, _decreasePositionDuration)
                .OnComplete(() =>
                {
                    _meshRenderer.material = _targetMaterials["Green"];
                    _isPressing = false;
                }));
            
            Pressed?.Invoke();
        }
    }
}