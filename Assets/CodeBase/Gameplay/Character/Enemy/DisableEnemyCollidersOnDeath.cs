using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CodeBase.Gameplay.Character.Enemy
{
    [RequireComponent(typeof(EnemyHealth))]
    public class DisableEnemyCollidersOnDeath : MonoBehaviour
    { 
        private List<Collider> _colliders = new();

        private IHealth _enemyHealth;

        private void Awake()
        {
            _enemyHealth = GetComponent<IHealth>();

            foreach (var collider in GetComponentsInChildren<Collider>())
            {
                _colliders.Add(collider);
            }
        }

        private void OnEnable() => 
            _enemyHealth.ValueZeroReached += DisableAll;

        private void OnDisable() => 
            _enemyHealth.ValueZeroReached -= DisableAll;

        private void DisableAll()
        {
            _colliders.ForEach(x=>x.enabled = false);
            _enemyHealth.Enabled = false;
        }
    }
}