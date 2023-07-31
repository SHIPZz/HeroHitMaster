using System.Collections.Generic;
using CodeBase.Enums;
using CodeBase.Services.Providers;

namespace CodeBase.UI.Windows
{
    public class WindowService
    {
        private readonly Dictionary<WindowTypeId, Window> _windows;
        private readonly Dictionary<WindowTypeId, Window> _hudWindows;
        private readonly List<Window> _allWindows;

        public WindowService(WindowProvider windowProvider)
        {
            _windows = windowProvider.Windows;
            _allWindows = windowProvider.AllWindows;
        }

        public void Close(WindowTypeId windowTypeId) =>
            _windows[windowTypeId].Close(true);
    
        public void Open(WindowTypeId windowTypeId) =>
            _windows[windowTypeId].Open();

        public void CloseAll()
        {
            foreach (var window in _allWindows) 
                window.Close(false);
        }
    }
}
