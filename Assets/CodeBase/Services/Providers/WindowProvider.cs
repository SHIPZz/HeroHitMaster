using System.Collections.Generic;
using CodeBase.Enums;
using CodeBase.UI.Windows;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

namespace CodeBase.Services.Providers
{
    public class WindowProvider : SerializedMonoBehaviour
    {
        [OdinSerialize] private Dictionary<WindowTypeId, Window> _windows;

        public Dictionary<WindowTypeId, Window> Windows => _windows;
    }
}