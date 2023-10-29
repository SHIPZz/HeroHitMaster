using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.Infrastructure
{
    public class LoadingSliderPresenter : MonoBehaviour
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private SliderValueText _sliderValueText;

        private void OnEnable() => 
            _slider.onValueChanged.AddListener(_sliderValueText.SetValue);

        private void OnDisable()
        {
            _slider.onValueChanged.RemoveListener(_sliderValueText.SetValue);
            _sliderValueText.SetValue(0);
        }
    }
}