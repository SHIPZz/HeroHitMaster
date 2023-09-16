using System;
using System.Collections;
using CodeBase.Gameplay.Camera;
using CodeBase.Gameplay.Collision;
using CodeBase.Services.Providers;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace CodeBase.Gameplay.Character.Players
{
    [RequireComponent(typeof(JumpOnTriggerEntered), typeof(TriggerObserver))]
    public class RotatePlayerOnJump : MonoBehaviour
    {
        [SerializeField] private Vector3 _rotateDirection;

        private float _duration = 1f;
        private JumpOnTriggerEntered _jumpOnTriggerEntered;
        private IProvider<CameraData> _cameraDataProvider;
        private TriggerObserver _triggerObserver;
        private GameObject _player;

        [Inject]
        private void CameraProvider(IProvider<CameraData> provider)
        {
            _cameraDataProvider = provider;
        }

        private void Awake()
        {
            _jumpOnTriggerEntered = GetComponent<JumpOnTriggerEntered>();
            _triggerObserver = GetComponent<TriggerObserver>();
        }

        private void OnEnable()
        {
            _jumpOnTriggerEntered.Jumped += Do;
            _triggerObserver.Entered += SetPlayer;
        }

        private void OnDisable()
        {
            _jumpOnTriggerEntered.Jumped -= Do;
        }

        private void Do()
        {
            _player.transform.DORotate(Vector3.right * -360, 3f);
            // StartCoroutine(RotatePlayerAndCamera());
        }

        private IEnumerator RotatePlayerAndCamera()
        {
            float startTime = Time.time;
            float endTime = startTime + 1f;
            UnityEngine.Camera camera = _cameraDataProvider.Get().Camera;

            while (Time.time < endTime)
            {
                float t = (Time.time - startTime) / 1f;
                float rotationAngle = Mathf.Lerp(0f, -360f, t);

                camera.transform.rotation = Quaternion.Euler(camera.transform.eulerAngles.x, rotationAngle,
                    camera.transform.eulerAngles.z);

                _player.transform.rotation = Quaternion.Euler(_player.transform.eulerAngles.x, rotationAngle,
                    _player.transform.eulerAngles.z);

                yield return null;
            }

            camera.transform.rotation = Quaternion.Euler(camera.transform.eulerAngles.x, 0f,
                camera.transform.eulerAngles.z);
            _player.transform.rotation = Quaternion.Euler(_player.transform.eulerAngles.x, 0f,
                _player.transform.eulerAngles.z);
        }

        private void SetPlayer(Collider player)
        {
            _player = player.gameObject;
        }
    }
}