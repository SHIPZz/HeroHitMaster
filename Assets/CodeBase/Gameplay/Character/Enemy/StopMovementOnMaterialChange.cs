using System;
using CodeBase.Gameplay.MaterialChanger;
using UnityEngine;
using Zenject;

namespace CodeBase.Gameplay.Character.Enemy
{
    public class StopMovementOnMaterialChange : IInitializable, IDisposable
    {
        private readonly IMaterialChanger _materialChanger;
        private readonly EnemyFollower _enemyFollower;

        public StopMovementOnMaterialChange(IMaterialChanger materialChanger, EnemyFollower enemyFollower)
        {
            _materialChanger = materialChanger;
            _enemyFollower = enemyFollower;
        }

        public void Initialize() => 
        _materialChanger.StartedChanged += DisableMovement;

        public void Dispose() => 
            _materialChanger.StartedChanged -= DisableMovement;

        private void DisableMovement() => 
            _enemyFollower.Block();
    }
}