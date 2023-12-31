using CodeBase.Gameplay.Character.Players;
using CodeBase.Services.Providers;
using UnityEngine;
using Zenject;

namespace CodeBase.Gameplay.Camera
{
    public class PlayerCameraFollower : MonoBehaviour
    {
        private IProvider<Player> _playerProvider;
        private Player _player;

        [Inject]
        private void Construct(IProvider<Player> playerProvider) => 
            _playerProvider = playerProvider;

        private void LateUpdate()
        {
            if (_playerProvider.Get() is null)
                return;
            
            transform.position = _playerProvider.Get().Head.position;
        }
    }
}