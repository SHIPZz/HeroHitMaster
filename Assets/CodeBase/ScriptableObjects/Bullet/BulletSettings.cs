using System;
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
        [OdinSerialize] public Dictionary<BulletTypeId, Func<string>> BulletPathes { get; private set; }

        [field: OdinSerialize] public Dictionary<WeaponTypeId, string> BulletPathesByWeapon { get; private set; }
        
        [field: OdinSerialize] public Dictionary<WeaponTypeId, BulletTypeId> BulletsByWeapon { get; private set; }
    }
}