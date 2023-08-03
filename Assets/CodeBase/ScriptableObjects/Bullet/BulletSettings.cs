using System.Collections.Generic;
using CodeBase.Enums;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace CodeBase.ScriptableObjects.Bullet
{
    [CreateAssetMenu(fileName = "BulletSettings", menuName = "Gameplay/BulletSettings")]
    public class BulletSettings : SerializedScriptableObject
    {
        [field: OdinSerialize] public Dictionary<WeaponTypeId, BulletTypeId> BulletIdsByWeapon { get; private set; }
    }
}