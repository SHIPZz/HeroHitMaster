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

        private void Start()
        {
            Sequence sequence = DOTween.Sequence();

            sequence.Append(_rectTransform.DOSizeDelta(new Vector2(_targetWidth, _targetHeight), _duration)
                .SetEase(Ease.OutQuad)); 

            sequence.Append(_rectTransform.DOSizeDelta(new Vector2(_startWidth, _startHeight), _duration)
                .SetEase(Ease.OutQuad)); 

            sequence.SetLoops(-1);

            sequence.Play();
        }
    }
}