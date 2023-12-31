﻿using System;
using CodeBase.Enums;
using CodeBase.Gameplay.Weapons;
using CodeBase.Infrastructure;
using CodeBase.Services.Providers;
using Zenject;

namespace CodeBase.UI.Windows.Play
{
    public class PlayWindowPresenter : IInitializable, IDisposable
    {
        private readonly WindowService _windowService;
        private readonly PlayButtonView _playButtonView;

        public PlayWindowPresenter(WindowService windowService, PlayButtonView playButtonView)
        {
            _playButtonView = playButtonView;
            _windowService = windowService;
        }

        public void Initialize()
        {
            _playButtonView.Clicked += Disable;
        }

        public void Dispose()
        {
            _playButtonView.Clicked -= Disable;
        }

        private void Disable() =>
            _windowService.Close(WindowTypeId.Play);
    }
}