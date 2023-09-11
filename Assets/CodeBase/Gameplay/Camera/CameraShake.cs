using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CodeBase.Gameplay.Camera
{
    public class CameraShake : MonoBehaviour
    {
        [Header("Recoil")] [SerializeField] private float _tension = 10;
        [SerializeField] private float _damping = 10;
        [SerializeField] private float _impulse = 10;

        [Header("Noise")] private float _perlinNoiseTimeScale;

        private Vector3 _shakeAngles;
        private Vector3 _recoilAngles;
        private Vector3 _recoilVelocity;

        private float _amplitude = 5;
        private float _shakeTimer = -1;
        private float _duration = 5;
        private bool _canShake = false;
        private Transform _transform;
        private Vector3 _initalRotation;

        private void Awake()
        {
            _transform = GetComponent<CameraData>().Camera.transform;
            _initalRotation = transform.eulerAngles;
        }

        private void Update()
        {
            UpdateShake();
            UpdateRecoil();

            _transform.localEulerAngles = _shakeAngles + _recoilAngles;
        }
        
        private void UpdateShake()
        {
            if (!_canShake)
            {
                _shakeAngles = Vector3.Lerp(_shakeAngles, _initalRotation, 3 * Time.deltaTime);
                return;
            }

            float time = Time.time * _perlinNoiseTimeScale;
            _shakeAngles.x = Mathf.PerlinNoise(time, 0f);
            _shakeAngles.y = Mathf.PerlinNoise(0, time);
            _shakeAngles.z = Mathf.PerlinNoise(time, time);

            _shakeAngles *= _amplitude;
        }

        private void UpdateRecoil()
        {
            _recoilAngles += _recoilVelocity * Time.deltaTime;
            _recoilVelocity += -_recoilAngles * Time.deltaTime * _tension;
            _recoilVelocity = Vector3.Lerp(_recoilVelocity, Vector3.zero, Time.deltaTime * _damping);
        }

        public void MakeShake(float perlinNoiseTimeScale)
        {
            if (_perlinNoiseTimeScale > perlinNoiseTimeScale)
                return;

            _perlinNoiseTimeScale = perlinNoiseTimeScale;
            _canShake = true;
            DOTween.Sequence().AppendInterval(1f).OnComplete(DisableShake);
        }

        private void DisableShake()
        {
            _canShake = false;
        }

        public void MakeRecoil()
        {
            var impulse = -Vector3.right * Random.Range(-_impulse * 0.5f, _impulse) +
                          Vector3.up * Random.Range(-_impulse, _impulse) / 4f;
            _recoilVelocity += impulse;
        }
    }
}