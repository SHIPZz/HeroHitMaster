﻿using CodeBase.Enums;
using UnityEngine;

namespace CodeBase.Gameplay
{
    public class DeathEnemyEffect : MonoBehaviour
    {
        [field: SerializeField] public EnemyTypeId EnemyTypeId { get; private set; }
        [field: SerializeField] public ParticleSystem DieEffect { get; private set; }
    }
}