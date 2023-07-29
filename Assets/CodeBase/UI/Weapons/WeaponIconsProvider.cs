using System.Collections.Generic;
using Enums;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

namespace Weapons
{
    public class WeaponIconsProvider : SerializedMonoBehaviour
    {
        [OdinSerialize] public Dictionary<WeaponTypeId, WeaponSelectorView> Icons { get; private set; }
    }
}