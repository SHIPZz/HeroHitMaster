using System.Collections.Generic;
using CodeBase.Enums;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace CodeBase.Installers.ScriptableObjects.Gun
{
    [CreateAssetMenu(fileName = "BulletSettings", menuName = "Gameplay/BulletSettings")]
    public class BulletSettings : SerializedScriptableObject
    {
        [field: OdinSerialize] public Dictionary<BulletTypeId, string> BulletPathes { get; private set; }
        [field: OdinSerialize] public Dictionary<WeaponTypeId, string> BulletPathesByWeapon { get; private set; }
        [field: OdinSerialize] public Dictionary<WeaponTypeId, BulletTypeId> BulletsByWeapon{ get; private set; }
        [SerializeField] public List<BulletTypeId> BulletTypeIds;
    }
}