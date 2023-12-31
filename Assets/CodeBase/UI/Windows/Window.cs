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

        public event Action StartedToOpen;
        public event Action StartedToClose;
        public event Action Opened;
        public event Action Closed;

        private void Awake() => 
            transform.DOScaleX(_startScaleX, _startDuration).SetUpdate(true);

        public void Open()
        {
            StartedToOpen?.Invoke();
            
            gameObject.SetActive(true);
            gameObject.transform
                .DOScaleX(_targetScaleX, _targetOpenDuration)
                .OnComplete(() => Opened?.Invoke()).SetAutoKill(true).SetUpdate(true);
        }

        public void Close(bool withAnimation)
        {
            StartedToClose?.Invoke();
            
            if (!withAnimation)
            {
                CloseQuickly();
                return;
            }

            gameObject.transform
                .DOScaleX(0, _targetCloseDuration)
                .OnComplete(() =>
                {
                    gameObject.SetActive(false);
                    Closed?.Invoke();
                })
                .SetAutoKill(true)
                .SetUpdate(true);
        }

        private void CloseQuickly()
        {
            transform.DOScaleX(0, 0).SetUpdate(true);
            gameObject.SetActive(false);
            Closed?.Invoke();
        }
    }
}