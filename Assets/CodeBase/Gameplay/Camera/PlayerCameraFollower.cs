using CodeBase.Gameplay.Character.Players;
using CodeBase.Installers.ScriptableObjects.PlayerCamera;
using CodeBase.Services.Providers;
using UnityEngine;
using Zenject;

namespace CodeBase.Gameplay.Camera
{
    public class PlayerCameraFollower : MonoBehaviour
    {
        private PlayerProvider _playerProvider;
        private Player _player;
        private Transform _playerHead;
        private Vector3 _targetCameraPosition;
        private PlayerCameraSettings _playerCameraSettings;

        [Inject]
        private void Construct(PlayerProvider playerProvider, PlayerCameraSettings playerCameraSettings)
        {
            _playerProvider = playerProvider;
            _playerCameraSettings = playerCameraSettings;
            _playerHead = _playerProvider.CurrentPlayer.Head;
        }

        private void LateUpdate()
        {
            _targetCameraPosition = _playerCameraSettings.CameraPlayerPositions[_playerProvider.CurrentPlayer.PlayerTypeId];
            _targetCameraPosition.x = _playerProvider.CurrentPlayer.Head.position.x;
            transform.position = _targetCameraPosition;
        }
    }
}