using System;
using UnityEngine;
using UnityEngine.AI;
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
            Dead?.Invoke(_enemy);
            gameObject.SetActive(false);
        }
    }
}