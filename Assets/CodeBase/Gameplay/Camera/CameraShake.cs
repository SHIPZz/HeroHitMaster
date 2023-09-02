using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CodeBase.Gameplay.Camera
{
    public class CameraShake : MonoBehaviour
    {
        [Header("Noise")] [SerializeField] private float _perlinNoiseTimeScale;

        [Header("Recoil")] [SerializeField] private float _tension = 10;
        [SerializeField] private float _damping = 10;
        [SerializeField] private float _impulse = 10;

        private Vector3 _shakeAngles = new();
        private Vector3 _recoilAngles = new();
        private Vector3 _recoilVelocity = new();

        private float _amplitude = 5;
        private float _shakeTimer = -1;
        private float _duration = 5;
        private bool _canShake = false;

        private void Update()
        {
            UpdateShake();
            UpdateRecoil();

            transform.localEulerAngles = _shakeAngles + _recoilAngles;
        }

        private void UpdateShake()
        {
            if (!_canShake)
            {
                _shakeAngles = Vector3.Lerp(_shakeAngles, Vector3.zero, 3 * Time.deltaTime);
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

        public void MakeShake()
        {
            _canShake = true;
            DOTween.Sequence().AppendInterval(1f).OnComplete(DisableShake);
        }

        private void DisableShake() => 
        _canShake = false;

        public void MakeRecoil()
        {
            var impulse = -Vector3.right * Random.Range(-_impulse * 0.5f, _impulse) +
                          Vector3.up * Random.Range(-_impulse, _impulse) / 4f;
            _recoilVelocity += impulse;
        }
    }
}