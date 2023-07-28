using System;
using UnityEngine;
using UnityEngine.UI;

namespace Windows.Audio
{
    public class AudioView : MonoBehaviour
    {
        [SerializeField] private Slider _slider;

        public event Action<float> ValueChanged;

        private void OnEnable() => 
            _slider.onValueChanged.AddListener(SliderValueChanged);

        private void OnDisable() => 
            _slider.onValueChanged.RemoveListener(SliderValueChanged);

        private void SliderValueChanged(float value) =>
            ValueChanged?.Invoke(value);
    }
}