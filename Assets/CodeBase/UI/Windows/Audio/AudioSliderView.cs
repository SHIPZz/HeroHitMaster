using System;
using CodeBase.Enums;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Windows.Audio
{
    public class AudioSliderView : MonoBehaviour
    {
        [field: SerializeField] public AudioViewTypeId AudioViewTypeId { get; private set; }
        [SerializeField] private Slider _slider;

        public Slider Slider => _slider;

        public event Action<float> ValueChanged;

        private void OnEnable() =>
            _slider.onValueChanged.AddListener(SliderValueChanged);

        private void OnDisable() =>
            _slider.onValueChanged.RemoveListener(SliderValueChanged);

        private void SliderValueChanged(float value) =>
            ValueChanged?.Invoke(value);
    }
}