using CodeBase.Gameplay.Character;
using UnityEngine;

namespace CodeBase.Gameplay.Camera
{
    public class RotateCameraPresenter : MonoBehaviour
    {
        private IHealth _playerHealth;
        private RotateCamera _rotateCamera;
        private IHealable _playerHeal;

        private void Awake() => 
            _rotateCamera = GetComponent<RotateCamera>();

        private void OnDisable()
        {
            _playerHealth.ValueZeroReached += _rotateCamera.Do;
            _playerHeal.Recovered -= _rotateCamera.ReturnLastRotation;
        }

        public void Init(IHealth playerHealth)
        {
            _playerHealth = playerHealth;
            _playerHealth.ValueZeroReached += _rotateCamera.Do;
            _playerHeal = _playerHealth.GameObject.GetComponent<IHealable>();
            _playerHeal.Recovered += _rotateCamera.ReturnLastRotation;
        }
    }
}