using CodeBase.Gameplay.Character.Players;
using UnityEngine;

namespace CodeBase.Gameplay.ExplosionBarrel
{
    [RequireComponent(typeof(ExplosionBarrel))]
    public class ActivateBarrelOnPlayerEnter : MonoBehaviour
    {
        [SerializeField] private AggroZone _aggroZone;

        private ExplosionBarrel _explosionBarrel;
        private Collider _collider;

        private void Awake()
        {
            _collider = GetComponent<Collider>();
            _collider.enabled = false;
        }

        private void OnEnable() =>
            _aggroZone.PlayerEntered += Activate;

        private void OnDisable() =>
            _aggroZone.PlayerEntered -= Activate;

        private void Activate(Player obj) =>
            _collider.enabled = true;
    }
}