using System;
using CodeBase.Services.Inputs.InputService;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace CodeBase.Gameplay.Character.Players.Shoot
{
    public class PlayerShootInput : MonoBehaviour
    {
        private IInputService _inputService;

        private bool _isBlocked = true;

        public event Action<Vector2> Fired;

        [Inject]
        private void Construct(IInputService inputService) =>
            _inputService = inputService;

        private void Update()
        {
            if (!_inputService.PlayerFire.WasPressedThisFrame() || EventSystem.current.IsPointerOverGameObject() ||
                _isBlocked)
                return;

            Fired?.Invoke(_inputService.MousePosition);
        }

        public void Block() =>
            _isBlocked = true;

        public void UnBlock() =>
            _isBlocked = false;
    }
}