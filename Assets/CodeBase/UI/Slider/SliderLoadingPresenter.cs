using System;
using CodeBase.UI.Windows;
using TMPro;

namespace CodeBase.UI.Slider
{
    public class SliderLoadingPresenter : IDisposable
    {
        private readonly WindowService _windowService;
        private readonly SliderLoading _sliderLoading;
        private readonly TextMeshProUGUI _loadingText;

        public SliderLoadingPresenter(SliderLoading sliderLoading, TextMeshProUGUI loadingText)
        {
            _loadingText = loadingText;
            _sliderLoading = sliderLoading;
            _sliderLoading.ValueChanged += SetLoadingText;
        }

        public void Dispose() => 
            _sliderLoading.ValueChanged -= SetLoadingText;

        private void SetLoadingText(int obj) => 
            _loadingText.text = $"{obj} %";
    }
}