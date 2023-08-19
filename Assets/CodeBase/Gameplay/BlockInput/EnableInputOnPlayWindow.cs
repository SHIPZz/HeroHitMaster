using System;
using CodeBase.Enums;
using CodeBase.Services.Inputs.InputService;
using CodeBase.Services.Providers;
using CodeBase.UI.Windows;
using Zenject;

namespace CodeBase.Gameplay.BlockInput
{
    public class EnableInputOnPlayWindow : IInitializable, IDisposable
    {
        private readonly IInputService _inputService;
        private readonly Window _playWindow;

        public EnableInputOnPlayWindow(WindowProvider windowProvider, IInputService inputService)
        {
            _playWindow = windowProvider.Windows[WindowTypeId.Play];
            _inputService = inputService;
        }

        public void Initialize()
        {
            _playWindow.Opened += EnableInput;
            _inputService.PlayerActions.Disable();
        }

        public void Dispose()
        {
            _playWindow.Opened -= EnableInput;
        }

        private void EnableInput() => 
            _inputService.PlayerActions.Enable();
    }
}