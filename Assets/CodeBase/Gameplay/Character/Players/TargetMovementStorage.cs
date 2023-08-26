using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Gameplay.Character.Players
{
    public class TargetMovementStorage : MonoBehaviour
    {
        [field: SerializeField] public List<Transform> Targets { get; private set; }
    }
}