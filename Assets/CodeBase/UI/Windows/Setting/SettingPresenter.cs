using System;
using CodeBase.Enums;
using CodeBase.Services.Pause;
using Zenject;

namespace CodeBase.UI.Windows.Setting
{
    public class SettingPresenter : IInitializable, IDisposable
    {
        private readonly SettingView _settingView;
        private readonly WindowService _windowService;
        private IPauseService _pauseService;

        public SettingPresenter(SettingView settingView, WindowService windowService, IPauseService pauseService)
        {
            _pauseService = pauseService;
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

        private void Open()
        {
            _windowService.CloseAll(() => _windowService.Open(WindowTypeId.SettingWindow));
        }

        private void Close() => 
        _windowService.CloseAll(() => _windowService.Open(WindowTypeId.Play));
    }
}