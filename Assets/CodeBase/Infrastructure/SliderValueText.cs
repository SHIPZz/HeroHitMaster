using TMPro;
using UnityEngine;

namespace CodeBase.Infrastructure
{
    public class SliderValueText : MonoBehaviour
    {
        private TextMeshProUGUI _text;

        private void Awake()
        {
            _text = GetComponent<TextMeshProUGUI>();
        }

        public void SetValue(float value) =>
            _text.text = $"{value} %";
    }
}