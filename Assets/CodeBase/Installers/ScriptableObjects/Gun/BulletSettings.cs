using System.Collections.Generic;
using Enums;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

[CreateAssetMenu(fileName = "BulletSettings", menuName = "Gameplay/BulletSettings")]
public class BulletSettings : SerializedScriptableObject
{
    [field: OdinSerialize] public Dictionary<WeaponTypeId, string> BulletPathes { get; private set; }
}