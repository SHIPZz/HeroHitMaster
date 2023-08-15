using System;
using System.Collections.Generic;
using System.Linq;
using CodeBase.Services.Inputs.InputService;
using CodeBase.Services.Providers;
using CodeBase.UI.Windows;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace CodeBase.Gameplay.BlockInput
{
    public class BlockShootInputOnUI : IInitializable, ITickable, IDisposable
    {
        private readonly IInputService _inputService;
        private readonly List<Window> _windows;
        private bool _isDisabled = false;

        public BlockShootInputOnUI(IInputService inputService, WindowProvider windowsProvider)
        {
            _inputService = inputService;
            _windows = windowsProvider.Windows.Values.ToList();
        }

        public void Initialize()
        {
            _windows.ForEach(x =>
            {
                x.StartedToOpen += DisableInput;
                x.Closed += EnableInput;
            });
        }

        public void Tick()
        {
            if (EventSystem.current.IsPointerOverGameObject())
                _inputService.PlayerFire.Disable();
            
            if(!EventSystem.current.IsPointerOverGameObject() && _isDisabled == false)
                _inputService.PlayerFire.Enable();
        }

        public void Dispose()
        {
            _windows.ForEach(x =>
            {
                x.StartedToOpen -= DisableInput;
                x.Closed -= EnableInput;
            });
        }

        private void EnableInput()
        {
            _inputService.PlayerFire.Enable();
            _isDisabled = false;
        }
        
        private void DisableInput()
        {
            _inputService.PlayerFire.Disable();
            _isDisabled = true;
        }
    }
}