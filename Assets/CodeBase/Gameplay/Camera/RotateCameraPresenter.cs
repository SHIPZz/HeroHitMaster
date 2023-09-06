using System;
using CodeBase.Gameplay.Character;
using CodeBase.Gameplay.Character.Players;
using CodeBase.Services.Providers;
using UnityEngine;
using Zenject;

namespace CodeBase.Gameplay.Camera
{
    public class RotateCameraPresenter : IDisposable
    {
        private IHealth _playerHealth;
        private RotateCamera _rotateCamera;
        private IHealable _playerHeal;
        private PlayerProvider _playerProvider;
        private Player _player;

        public RotateCameraPresenter(IProvider<PlayerProvider> playerProvider)
        {
            _playerProvider = playerProvider.Get();
            _playerProvider.Changed += SetPlayer;
        }

        public void Dispose()
        {
            _playerProvider.Changed -= SetPlayer;
        }

        public void Init(RotateCamera rotateCamera)
        {
            _rotateCamera = rotateCamera;
            Subscribe();
            Debug.Log(_playerHealth.GameObject.name);
        }

        private void Subscribe()
        {
            _playerHealth = _player.GetComponent<PlayerHealth>();
            _playerHealth.ValueZeroReached += _rotateCamera.Do;
            _playerHeal = _playerHealth.GameObject.GetComponent<IHealable>();
            _playerHeal.Recovered += _rotateCamera.ReturnLastRotation;
        }

        private void SetPlayer(Player player)
        {
            _player = player;
        }
    }
}