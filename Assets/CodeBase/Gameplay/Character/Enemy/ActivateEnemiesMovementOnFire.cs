using System;
using System.Collections.Generic;
using CodeBase.Services.Inputs.InputService;
using UnityEngine.InputSystem;

namespace CodeBase.Gameplay.Character.Enemy
{
    public class ActivateEnemiesMovementOnFire : IDisposable
    {
        private readonly IInputService _inputService;
        private readonly List<EnemyFollower> _enemyFollowers = new();
        private bool _isFired;

        public ActivateEnemiesMovementOnFire(IInputService inputService) => 
            _inputService = inputService;

        public void Init(List<Enemy> enemies)
        {
            _inputService.PlayerFire.performed += ActivateMovement;

            FillList(enemies);
        }

        public void Dispose() => 
            _inputService.PlayerFire.performed -= ActivateMovement;

        private void ActivateMovement(InputAction.CallbackContext obj)
        {
            if (_isFired)
                return;
            
            _isFired = true;
            _enemyFollowers.ForEach(x => x.Unblock());
        }

        private void FillList(List<Enemy> enemies)
        {
            foreach (var enemy in enemies)
            {
                var enemyFollower = enemy.GetComponent<EnemyFollower>();
                _enemyFollowers.Add(enemyFollower);
            }
        }
    }
}