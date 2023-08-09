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

        public event Action Opened;
        public event Action Closed;

        private void Awake() => 
            transform.DOScaleX(_startScaleX, _startDuration);

        public void Open() =>
            DOTween.Sequence().OnComplete(() =>
            {
                gameObject.SetActive(true);
                gameObject.transform.DOScaleX(_targetScaleX, _targetOpenDuration)
                    .OnComplete(() => Opened?.Invoke());
            });

        public void Close(bool withAnimation)
        {
            if (CloseWithoutAnimation(withAnimation)) 
                return;

            DOTween.Sequence().OnComplete(() =>
            {
                gameObject.SetActive(false);
                gameObject.transform.DOScaleX(0, _targetCloseDuration)
                    .OnComplete(() => Closed?.Invoke());
            });
        }

        private bool CloseWithoutAnimation(bool withAnimation)
        {
            if (withAnimation) 
                return false;
            
            transform.DOScaleX(0, 0);
            gameObject.SetActive(false);
            Closed?.Invoke();
            return true;

        }
    }
}