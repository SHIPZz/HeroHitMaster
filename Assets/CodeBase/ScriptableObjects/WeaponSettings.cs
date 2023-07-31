using System.Collections.Generic;
using CodeBase.Enums;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CodeBase.ScriptableObjects
{
    [CreateAssetMenu(menuName = "Gameplay/WeaponSettings", fileName = "WeaponSettings")]
    public class WeaponSettings : SerializedScriptableObject
    {
        [field: SerializeField] public List<WeaponTypeId> WeaponTypeIds { get; private set; }
        [field: SerializeField] public Dictionary<WeaponTypeId, string> WeaponPathes { get; private set; }
        [field: SerializeField] public Dictionary<WeaponTypeId, int> WeaponPrices { get; private set; }
    }
}