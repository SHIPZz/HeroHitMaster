using System.Collections.Generic;
using Enums;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Services.Providers
{
    public class WindowProvider : SerializedMonoBehaviour
    {
        [OdinSerialize] public Dictionary<WindowTypeId, Window> SelectorWindows { get; private set; }
        [OdinSerialize] public Dictionary<WindowTypeId, Window> HudWindows { get; private set; }

    }
}