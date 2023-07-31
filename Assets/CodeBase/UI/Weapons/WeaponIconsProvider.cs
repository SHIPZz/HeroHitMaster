using System.Collections.Generic;
using CodeBase.Enums;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

namespace CodeBase.UI.Weapons
{
    public class WeaponIconsProvider : SerializedMonoBehaviour
    {
        [OdinSerialize] public Dictionary<WeaponTypeId, WeaponSelectorView> Icons { get; private set; }
    }
}