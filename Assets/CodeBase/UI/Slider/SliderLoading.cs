using System;
using UnityEngine;

namespace CodeBase.UI.Slider
{
    public class SliderLoading : MonoBehaviour
    {
        private UnityEngine.UI.Slider _slider;

        public event Action<int> ValueChanged;
        public event Action Loaded;

        private void Awake() => 
            _slider = GetComponent<UnityEngine.UI.Slider>();

        private void OnEnable() => 
            _slider.onValueChanged.AddListener(OnValueChanged);

        private void OnDisable() =>
            _slider.onValueChanged.RemoveListener(OnValueChanged);

        private void OnValueChanged(float value) => 
            ValueChanged?.Invoke((int)value);
    }
}