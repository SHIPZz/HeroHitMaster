using System;
using DG.Tweening;
using UnityEngine;

namespace CodeBase.UI.Windows.RectTransformAnimations
{
    public class RectTransformAnimationOnAwake : MonoBehaviour
    {
        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private float _startWidth;
        [SerializeField] private float _startHeight;
        [SerializeField] private float _targetWidth;
        [SerializeField] private float _targetHeight;
        [SerializeField] private float _duration;
        [SerializeField] private bool _setUpdate = true;
        [SerializeField] private bool _takeValuesFromTransform;

        private void Start()
        {
            if (_takeValuesFromTransform)
            {
                _startWidth = _rectTransform.rect.width;
                _startHeight = _rectTransform.rect.height;
            }
            
            Sequence sequence = DOTween.Sequence();

            sequence.Append(_rectTransform.DOSizeDelta(new Vector2(_targetWidth, _targetHeight), _duration)
                .SetEase(Ease.OutQuad)).SetUpdate(_setUpdate); 

            sequence.Append(_rectTransform.DOSizeDelta(new Vector2(_startWidth, _startHeight), _duration)
                .SetEase(Ease.OutQuad)).SetUpdate(_setUpdate); 

            sequence.SetLoops(-1).SetUpdate(_setUpdate);

            sequence.Play().SetUpdate(_setUpdate);
        }
    }
}