using System;
using Services.Inputs;
using UnityEngine;
using Zenject;

namespace CodeBase.Gameplay.Character.Players.Shoot
{
    public class PlayerInput : ITickable
    {
        private readonly IInputService _inputService;

        public event Action<Vector2> Fired;

        public PlayerInput(IInputService inputService)
        {
            _inputService = inputService;
        }

        public void Tick()
        {
            if (!_inputService.PlayerFire.WasPressedThisFrame())
                return;

            Vector2 mousePosition = _inputService.MousePosition;
            
            Fired?.Invoke(mousePosition);
        }
    }
}