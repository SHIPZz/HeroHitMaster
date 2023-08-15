using System;
using CodeBase.Services.Inputs.InputService;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace CodeBase.Gameplay.Character.Enemy
{
    public class ActivateEnemyMovementOnFire : IInitializable, IDisposable
    {
        private readonly IInputService _inputService;
        private readonly EnemyFollower _enemyFollower;
        private bool _isFired;

        public ActivateEnemyMovementOnFire(IInputService inputService, EnemyFollower enemyFollower)
        {
            _enemyFollower = enemyFollower;
            _inputService = inputService;
        }

        public void Initialize()
        {
            _inputService.PlayerFire.performed += ActivateMovement;
            _enemyFollower.Block();
        }

        public void Dispose() =>
            _inputService.PlayerFire.performed -= ActivateMovement;

        private void ActivateMovement(InputAction.CallbackContext obj)
        {
            if (_isFired)
                return;
            
            _isFired = true;
            _enemyFollower.Unblock();
        }
    }
}