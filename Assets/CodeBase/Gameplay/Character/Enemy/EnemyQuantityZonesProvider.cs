using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Gameplay.Character.Enemy
{
    public class EnemyQuantityZonesProvider : MonoBehaviour
    {
        [field: SerializeField] public List<EnemyQuantityInZone> EnemyQuantityInZones { get; private set; }
    }
}