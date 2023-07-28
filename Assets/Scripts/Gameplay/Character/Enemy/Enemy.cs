using Enums;
using UnityEngine;

namespace Gameplay.Character.Enemy
{
    public class Enemy : MonoBehaviour
    {
        [field: SerializeField] public EnemyTypeId EnemyTypeId { get; private set; }
    }
}