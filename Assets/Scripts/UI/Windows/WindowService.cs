using System.Collections.Generic;
using Enums;
using Services.Providers;

namespace Windows
{
    public class WindowService
    {
        private readonly Dictionary<WindowTypeId, Window> _windows;
        private readonly Dictionary<WindowTypeId, Window> _hudWindows;
        private readonly Dictionary<WindowTypeId, Window> _selectorWindows;

        public WindowService(WindowProvider windowProvider)
        {
            _windows = windowProvider.Windows;
            _selectorWindows = windowProvider.SelectorWindows;
            // _windows = windowProvider.Windows;
            // _selectorWindows = windowProvider.SelectorWindows;
            // _hudWindows = windowProvider.HudWindows;
        }

        public void OpenHudWindow(WindowTypeId windowTypeId) =>
            Open(_hudWindows, windowTypeId);
    
        public void CloseHudWindow(WindowTypeId windowTypeId) =>
            Open(_hudWindows, windowTypeId);

        public void OpenHud() => 
            Open(_hudWindows);

        public void CloseHud() => 
            Close(_hudWindows);

        public Window Get(WindowTypeId windowTypeId) =>
            _windows[windowTypeId];

        public void OpenSelectorWindow(WindowTypeId windowTypeId) => 
            Open(_selectorWindows, windowTypeId);

        public void CloseSelectorWindow(WindowTypeId windowTypeId) => 
            Close(_selectorWindows, windowTypeId);

        public void Close(WindowTypeId windowTypeId) =>
            _windows[windowTypeId].Close();
    
        public void Open(WindowTypeId windowTypeId) =>
            _windows[windowTypeId].Open();

        private void Open(Dictionary<WindowTypeId, Window> windows, WindowTypeId windowTypeId)
        {
            Close(windows);
            windows[windowTypeId].Open();
        }

        private void Close(Dictionary<WindowTypeId, Window> windows, WindowTypeId windowTypeId) => 
            windows[windowTypeId].Close();

        private void Open(Dictionary<WindowTypeId, Window> windows)
        {
            foreach (Window window in windows.Values)
            {
                window.Open();
            }
        }

        private void Close(Dictionary<WindowTypeId, Window> windows)
        {
            foreach (Window window in windows.Values)
            {
                window.Close();
            }
        }
    }
}
