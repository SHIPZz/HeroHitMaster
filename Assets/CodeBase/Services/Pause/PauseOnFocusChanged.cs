using System;
using UnityEngine;
using Zenject;

namespace CodeBase.Services.Pause
{
    public class PauseOnFocusChanged : IInitializable, IDisposable
    {
        private readonly IPauseService _pauseService;

        public PauseOnFocusChanged(IPauseService pauseService)
        {
            _pauseService = pauseService;
        }

        public void Initialize()
        {
            Application.focusChanged += OnFocusChanged;
        }

        public void Dispose()
        {
            Application.focusChanged -= OnFocusChanged;
        }

        private void OnFocusChanged(bool focusChanged)
        {
            // if (!focusChanged)
            // {
            //     _pauseService.Pause();
            //     return;
            // }
            //
            // _pauseService.UnPause();
        }
    }
}