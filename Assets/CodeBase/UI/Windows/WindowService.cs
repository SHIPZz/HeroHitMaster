using System;
using System.Collections.Generic;
using CodeBase.Enums;
using CodeBase.Services.Providers;
using JetBrains.Annotations;
using UnityEngine;

namespace CodeBase.UI.Windows
{
    public class WindowService
    {
        private readonly Dictionary<WindowTypeId, Window> _windows;
        private readonly Dictionary<WindowTypeId, Window> _hudWindows;

        public WindowService(WindowProvider windowProvider) => 
            _windows = windowProvider.Windows;

        public void Close(WindowTypeId windowTypeId, [CanBeNull] Action callback)
        {
            _windows[windowTypeId].Close(true);
            _windows[windowTypeId].Closed += callback;
        }
        
        public void Close(WindowTypeId windowTypeId) => 
        _windows[windowTypeId].Close(true);

        public void Open(WindowTypeId windowTypeId) => 
            _windows[windowTypeId].Open();

        public void Open(WindowTypeId windowTypeId, Action callback)
        {
            _windows[windowTypeId].Open();
            _windows[windowTypeId].Opened += callback;
        }

        public void CloseAll()
        {
            foreach (var window in _windows.Values) 
                window.Close(false);
        }
        
        public void CloseAll(Action callback)
        {
            foreach (var window in _windows.Values) 
                window.Close(false);
            
            callback?.Invoke();
        }
    }
}
