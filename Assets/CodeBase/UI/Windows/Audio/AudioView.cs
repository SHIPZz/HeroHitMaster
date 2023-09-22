using System;
using CodeBase.Services.SaveSystems;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace CodeBase.UI.Windows.Audio
{
    public class AudioView : MonoBehaviour
    {
        [SerializeField] private UnityEngine.UI.Slider _slider;

        public UnityEngine.UI.Slider Slider => _slider;

        public event Action<float> ValueChanged;

        private void OnEnable() =>
            _slider.onValueChanged.AddListener(SliderValueChanged);

        private void OnDisable() =>
            _slider.onValueChanged.RemoveListener(SliderValueChanged);

        private void SliderValueChanged(float value) =>
            ValueChanged?.Invoke(value);
    }
}