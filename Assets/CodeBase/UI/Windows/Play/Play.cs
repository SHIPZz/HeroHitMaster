using System;
using CodeBase.Enums;
using CodeBase.Services.Inputs.InputService;
using UnityEngine.InputSystem;
using Zenject;

namespace CodeBase.UI.Windows.Play
{
    public class PlayWindowPresenter : IInitializable, IDisposable
    {
        private readonly WindowService _windowService;
        private readonly IInputService _inputService;

        public PlayWindowPresenter(WindowService windowService, IInputService inputService)
        {
            _inputService = inputService;
            _windowService = windowService;
        }

        public void Initialize()
        {
            // _windowService.CloseAll();
            // _windowService.Open(WindowTypeId.Play);
            _inputService.PlayerFire.performed += Disable;
        }

        public void Dispose() => 
        _inputService.PlayerFire.performed -= Disable;

        private void Disable(InputAction.CallbackContext obj) =>
            _windowService.Close(WindowTypeId.Play);
    }
}