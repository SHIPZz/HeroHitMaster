using System;
using CodeBase.Services.Inputs.InputService;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using Zenject;
using IInitializable = Zenject.IInitializable;

namespace CodeBase.Gameplay.Character.Players.Shoot
{
    public class PlayerShootInput : ITickable
    {
        private readonly IInputService _inputService;

        public event Action<Vector2> Fired;

        public PlayerShootInput(IInputService inputService) => 
            _inputService = inputService;

        public void Tick()
        {
            if (!_inputService.PlayerFire.WasPressedThisFrame() || EventSystem.current.IsPointerOverGameObject())
                return;
            
            Fired?.Invoke(_inputService.MousePosition);
        }
    }
}