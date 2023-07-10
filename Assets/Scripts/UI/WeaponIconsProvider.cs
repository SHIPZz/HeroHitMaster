using System.Collections.Generic;
using Enums;
using Services.Factories;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

namespace UI
{
    public class WeaponIconsProvider : SerializedMonoBehaviour
    {
        [OdinSerialize] public Dictionary<WeaponTypeId, WeaponSelectorView> Icons { get; private set; }
    }
}