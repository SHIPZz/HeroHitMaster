using Enums;
using UnityEngine;

namespace Gameplay.Character.Player
{
    public class Player : MonoBehaviour, IDamageable
    {
        [field: SerializeField] public Transform Head { get; private set; }
        [field: SerializeField] public PlayerTypeId PlayerTypeId { get; private set; }

        public void TakeDamage(int value)
        {
            
        }
    }
}
