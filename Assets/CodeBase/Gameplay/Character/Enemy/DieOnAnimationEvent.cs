using System;
using CodeBase.Enums;
using UnityEngine;
using Zenject;

namespace CodeBase.Gameplay.Character.Enemy
{
    public class DieOnAnimationEvent : MonoBehaviour
    {
        private Enemy _enemy;

        [Inject]
        private void Construct(Enemy enemy) =>
            _enemy = enemy;
        
        public event Action<Enemy> Dead;
        
        public void OnAnimationDead()
        {
            Destroy(gameObject);
            Dead?.Invoke(_enemy);
        }
    }
}