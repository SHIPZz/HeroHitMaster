using Cinemachine;
using Gameplay.Character.Player;
using Gameplay.Character.Players;
using Services.Providers;
using UnityEngine;
using Zenject;

namespace Gameplay.Camera
{
    public class PlayerCameraFollower : MonoBehaviour
    {
        [SerializeField] private float _smoothSpeed;
        [SerializeField] private Vector3 _offset;

        private PlayerProvider _playerProvider;
        private Player _player;

        [Inject]
        private void Construct(PlayerProvider playerProvider) =>
            _playerProvider = playerProvider;

        private void LateUpdate()
        {
            transform.position = _playerProvider.CurrentPlayer.Head.position + new Vector3(0, 0.1f, 0);
        }
    }
}