using UnityEngine;

namespace CodeBase.Gameplay.Character.Enemy
{
    [RequireComponent(typeof(EnemyHealth))]
    public class DisableHealthOnDeath : MonoBehaviour
    {
        private IHealth _enemyHealth;

        private void Awake() => 
            _enemyHealth = GetComponent<IHealth>();

        private void OnEnable() => 
            _enemyHealth.ValueZeroReached += DisableAll;

        private void OnDisable() => 
            _enemyHealth.ValueZeroReached -= DisableAll;

        private void DisableAll() => 
            _enemyHealth.Enabled = false;
    }
}