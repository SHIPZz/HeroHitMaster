using UnityEngine;

namespace CodeBase.Gameplay.Character.Players
{
    public class DisablePlayerOnDeath : MonoBehaviour
    {
        private IHealth _health;

        private void Awake() => 
            _health = GetComponent<IHealth>();

        private void OnEnable() => 
            _health.ValueZeroReached += Disable;

        private void OnDisable() => 
            _health.ValueZeroReached -= Disable;

        private void Disable() =>
            gameObject.SetActive(false);
    }
}