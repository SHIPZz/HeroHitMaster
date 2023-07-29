using System;
using DG.Tweening;
using Enums;
using UnityEngine;

namespace Windows
{
    public class Window : MonoBehaviour
    {
        [SerializeField] private float _startScaleX;
        [SerializeField] private float _targetScaleX;
        [SerializeField] private float _startDuration;
        [SerializeField] private float _targetOpenDuration;
        [SerializeField] private float _targetCloseDuration;

        [field: SerializeField] public WindowTypeId WindowTypeId { get; private set; }

        public event Action Opened;
        public event Action Closed;

        private void Awake() => 
            transform.DOScaleX(_startScaleX, _startDuration);

        public void Open() =>
            DOTween.Sequence().OnComplete(() =>
            {
                // gameObject.SetActive(true);
                gameObject.transform.DOScaleX(_targetScaleX, _targetOpenDuration)
                    .OnComplete(() => Opened?.Invoke());
            });

        public void Close() =>
            DOTween.Sequence().OnComplete(() =>
            {
                // gameObject.SetActive(false);
                gameObject.transform.DOScaleX(0, _targetCloseDuration)
                    .OnComplete(() =>Closed?.Invoke());
            });
    }
}