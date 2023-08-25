using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Gameplay.Character.Enemy
{
    [RequireComponent(typeof(EnemyHealth))]
    public class DisableEnemyCollidersOnDeath : MonoBehaviour
    {
        [SerializeField] private List<Collider> _colliders;

        private IHealth _enemyHealth;

        private void Awake() => 
            _enemyHealth = GetComponent<IHealth>();

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