using CodeBase.Gameplay.Character.Players;
using CodeBase.Services.Providers;
using UnityEngine;
using Zenject;

namespace CodeBase.Gameplay.Camera
{
    public class PlayerCameraFollower : MonoBehaviour
    {
        private PlayerProvider _playerProvider;
        private Player _player;

        [Inject]
        private void Construct(PlayerProvider playerProvider) =>
            _playerProvider = playerProvider;

        private void LateUpdate()
        {
            transform.position = _playerProvider.CurrentPlayer.Head.position + new Vector3(0, 0.2f, 0);
        }
    }
}