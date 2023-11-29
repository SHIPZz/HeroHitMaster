using System;
using CodeBase.Enums;
using DG.Tweening;
using UnityEngine;

namespace CodeBase.UI.Windows
{
    public class Window : MonoBehaviour
    {
        [SerializeField] private float _startScaleX;
        [SerializeField] private float _targetScaleX;
        [SerializeField] private float _startDuration;
        [SerializeField] private float _targetOpenDuration;
        [SerializeField] private float _targetCloseDuration;
        [field: SerializeField] public WindowTypeId WindowTypeId { get; private set; }

        private Tween _tween;

        public event Action StartedToOpen;
        public event Action Opened;
        public event Action Closed;

        private void Awake()
        {
            transform.DOScaleX(_startScaleX, _startDuration).SetUpdate(true);
        }

        public void OpenQuickly()
        {
            StartedToOpen?.Invoke();

            gameObject.SetActive(true);

            _tween?.Kill();
            _tween = gameObject.transform
                .DOScaleX(_targetScaleX, 0)
                .OnComplete(() => Opened?.Invoke()).SetUpdate(true).SetAutoKill(true);
        }

        public void Open()
        {
            StartedToOpen?.Invoke();

            gameObject.SetActive(true);

            _tween?.Kill();
            _tween = gameObject.transform
                .DOScaleX(_targetScaleX, _targetOpenDuration)
                .OnComplete(() => Opened?.Invoke()).SetUpdate(true).SetAutoKill(true);
        }

        public void Close(bool withAnimation)
        {
            _tween?.Kill();
            
            if (!withAnimation)
            {
                CloseQuickly();
                return;
            }

            _tween = gameObject.transform
                .DOScaleX(0, _targetCloseDuration)
                .OnComplete(() =>
                {
                    gameObject.SetActive(false);
                    Closed?.Invoke();
                })
                .SetUpdate(true).SetAutoKill(true);
        }

        private void CloseQuickly()
        {
          _tween =  transform.DOScaleX(0, 0).SetUpdate(true).SetAutoKill(true);
            gameObject.SetActive(false);
        }
    }
}