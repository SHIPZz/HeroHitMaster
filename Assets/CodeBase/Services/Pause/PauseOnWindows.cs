using System;
using System.Collections.Generic;
using System.Linq;
using CodeBase.Enums;
using CodeBase.Services.Providers;
using CodeBase.UI.Windows;
using Zenject;

namespace CodeBase.Services.Pause
{
    public class PauseOnWindows : IInitializable, IDisposable
    {
        private readonly Window _playWindow;
        private readonly IPauseService _pauseService;
        private readonly List<Window> _allWindows;

        public PauseOnWindows(IPauseService pauseService, WindowProvider windowProvider)
        {
            _playWindow = windowProvider.Windows[WindowTypeId.Play];
            _pauseService = pauseService;
            _allWindows = windowProvider.Windows.Values.ToList();
        }

        public void Initialize()
        {
            // _playWindow.Opened += _pauseService.UnPause;
            //
            // foreach (Window window in _allWindows)
            // {
            //     if(window.WindowTypeId == WindowTypeId.Play)
            //         continue;
            //         
            //     window.StartedToOpen += _pauseService.Pause;
            // }
        }

        public void Dispose()
        {
            // _playWindow.Opened -= _pauseService.UnPause;
            //
            // foreach (Window window in _allWindows)
            // {    
            //     if(window.WindowTypeId == WindowTypeId.Play)
            //         continue;
            //     
            //     window.StartedToOpen -= _pauseService.Pause;
            // }
        }
    }
}