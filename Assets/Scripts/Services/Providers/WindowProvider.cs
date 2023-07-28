using System.Collections.Generic;
using Windows;
using Enums;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Services.Providers
{
    public class WindowProvider : SerializedMonoBehaviour
    {
        [OdinSerialize] private Dictionary<WindowTypeId, Window> _windows;
        [OdinSerialize] private Dictionary<WindowTypeId, Window> _hudWindows;
        [OdinSerialize] private Dictionary<WindowTypeId, Window> _selectorWindows;

        public Dictionary<WindowTypeId, Window> Windows => _windows;

        public Dictionary<WindowTypeId, Window> HUDWindows => _hudWindows;

        public Dictionary<WindowTypeId, Window> SelectorWindows => _selectorWindows;
    }
}