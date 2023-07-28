﻿using System;
using Enums;
using Zenject;

namespace Windows.Setting
{
    public class SettingPresenter : IInitializable, IDisposable
    {
        private readonly SettingView _settingView;
        private readonly WindowService _windowService;

        public SettingPresenter(SettingView settingView, WindowService windowService)
        {
            _settingView = settingView;
            _windowService = windowService;
        }

        public void Initialize()
        {
            _settingView.OpenedButtonClicked += Open;
            _settingView.ClosedButtonClicked += Close;
        }

        public void Dispose()
        {
            _settingView.OpenedButtonClicked -= Open;
            _settingView.ClosedButtonClicked -= Close;
        }

        private void Open() => 
            _windowService.Open(WindowTypeId.SettingWindow);

        private void Close() => 
            _windowService.Close(WindowTypeId.SettingWindow);
    }
}