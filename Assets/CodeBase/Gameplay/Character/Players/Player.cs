using CodeBase.Enums;
using UnityEngine;

namespace CodeBase.Gameplay.Character.Players
{
    public class Player : MonoBehaviour
    {
        [field: SerializeField] public Transform Head { get; private set; }
        [field: SerializeField] public Transform RightHand { get; private set; }
        [field: SerializeField] public PlayerTypeId PlayerTypeId { get; private set; }
        
    }
}
