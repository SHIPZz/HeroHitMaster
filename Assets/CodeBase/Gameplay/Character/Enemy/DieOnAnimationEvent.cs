using System;
using UnityEngine;
using Zenject;

namespace CodeBase.Gameplay.Character.Enemy
{
    public class DieOnAnimationEvent : MonoBehaviour
    {
        private Enemy _enemy;
        
        public event Action<Enemy> Dead;

        [Inject]
        private void Construct(Enemy enemy) => 
            _enemy = enemy;

        public void OnAnimationDead()
        {
            Destroy(gameObject, 0.1f);
            Dead?.Invoke(_enemy);
        }
    }
}