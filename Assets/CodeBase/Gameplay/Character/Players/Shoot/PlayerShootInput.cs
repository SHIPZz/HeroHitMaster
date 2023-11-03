using System;
using CodeBase.Services.Inputs.InputService;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace CodeBase.Gameplay.Character.Players.Shoot
{
    public class PlayerShootInput : ITickable
    {
        private readonly IInputService _inputService;

        private bool _isBlocked = true;

        public event Action<Vector2> Fired;

        public PlayerShootInput(IInputService inputService) =>
            _inputService = inputService;

        public void Tick()
        {
            if (!_inputService.PlayerFire.WasPressedThisFrame() || EventSystem.current.IsPointerOverGameObject() ||
                _isBlocked)
                return;

            Fired?.Invoke(_inputService.MousePosition);
        }

        public void UnBlock() =>
            _isBlocked = false;
    }
}