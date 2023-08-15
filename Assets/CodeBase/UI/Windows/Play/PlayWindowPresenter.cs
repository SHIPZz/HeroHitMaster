using System;
using CodeBase.Enums;
using CodeBase.Infrastructure;
using CodeBase.Services.Inputs.InputService;
using UnityEngine.InputSystem;
using Zenject;

namespace CodeBase.UI.Windows.Play
{
    public class PlayWindowPresenter : IInitializable, IDisposable
    {
        private readonly WindowService _windowService;
        private readonly IInputService _inputService;
        private readonly ILoadingCurtain _loadingCurtain;

        public PlayWindowPresenter(WindowService windowService, IInputService inputService, ILoadingCurtain loadingCurtain)
        {
            _loadingCurtain = loadingCurtain;
            _inputService = inputService;
            _windowService = windowService;
        }

        public void Initialize()
        {
            _inputService.PlayerFire.performed += Disable;
            _loadingCurtain.Closed += Enable;
        }

        public void Dispose()
        {
            _inputService.PlayerFire.performed -= Disable;
            _loadingCurtain.Closed -= Enable;
        }

        private void Enable() => 
            _windowService.Open(WindowTypeId.Play);

        private void Disable(InputAction.CallbackContext obj) =>
            _windowService.Close(WindowTypeId.Play);
    }
}