using System.Collections.Generic;
using CodeBase.Enums;
using CodeBase.UI.Windows;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace CodeBase.Services.Providers
{
    public class WindowProvider : SerializedMonoBehaviour
    {
        [OdinSerialize] private Dictionary<WindowTypeId, Window> _windows;
        [OdinSerialize] private Dictionary<WindowTypeId, Window> _hudWindows;
        [OdinSerialize] private Dictionary<WindowTypeId, Window> _selectorWindows;
        [field: SerializeField] public List<Window> AllWindows;

        public Dictionary<WindowTypeId, Window> Windows => _windows;

        public Dictionary<WindowTypeId, Window> HUDWindows => _hudWindows;

        public Dictionary<WindowTypeId, Window> SelectorWindows => _selectorWindows;
    }
}