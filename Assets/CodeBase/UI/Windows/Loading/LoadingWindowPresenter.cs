using System;
using CodeBase.Enums;
using CodeBase.UI.Slider;
using Zenject;

namespace CodeBase.UI.Windows.Loading
{
    public class LoadingWindowPresenter : IInitializable, IDisposable
    {
        private readonly WindowService _windowService;
        private readonly SliderLoading _sliderLoading;

        public LoadingWindowPresenter(WindowService windowService, SliderLoading sliderLoading)
        {
            _sliderLoading = sliderLoading;
            _windowService = windowService;
            _sliderLoading.Loaded += Close;
        }

        public void Initialize() =>
            _windowService.Open(WindowTypeId.Loading);

        public void Dispose() => 
            _sliderLoading.Loaded -= Close;

        private void Close()
        {
            _windowService.Close(WindowTypeId.Loading);
            _windowService.Open(WindowTypeId.Play);
        }
    }
}