using Enums;
using Services.Providers;
using UnityEngine;

namespace Gameplay.Character.Player
{
    public class Player : MonoBehaviour, IDamageable
    {
        private WeaponsProvider _weaponsProvider;
        [field: SerializeField] public Transform Head { get; private set; }
        [field: SerializeField] public PlayerTypeId PlayerTypeId { get; private set; }

        public void TakeDamage(int value)
        {
            
        }
    }
}
