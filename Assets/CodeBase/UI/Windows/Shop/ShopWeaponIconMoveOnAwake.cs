using System.Collections.Generic;
using Agava.WebUtility;
using DG.Tweening;
using UnityEngine;

namespace CodeBase.UI.Windows.Shop
{
    [RequireComponent(typeof(RectTransform))]
    public class ShopWeaponIconMoveOnAwake : MonoBehaviour
    {
        [SerializeField] private float _duration = 1f;
        [SerializeField] private float _posY = 7.5f;
        [SerializeField] private List<ParticleSystem> _effects;
        private RectTransform _rectTransform;
        private Tween _animationTween;
        private Vector2 _initialAnchoredPosition;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            _initialAnchoredPosition = _rectTransform.anchoredPosition;
        }

        private void OnEnable()
        {
            StartAnimation();
            Application.focusChanged += OnFocusChanged;
            WebApplication.InBackgroundChangeEvent += OnFocusChanged;
        }

        private void OnDisable()
        {
            StopAnimation();
            Application.focusChanged -= OnFocusChanged;
            WebApplication.InBackgroundChangeEvent -= OnFocusChanged;
        }

        private void OnFocusChanged(bool hasFocus)
        {
            if (!hasFocus)
            {
                _animationTween.Pause();
                _rectTransform.anchoredPosition = _initialAnchoredPosition;
                _effects.ForEach(x => x.gameObject.SetActive(false));
                return;
            }

            _effects.ForEach(x => x.gameObject.SetActive(true));
            _animationTween.Restart();
        }

        private void StartAnimation()
        {
            _animationTween = _rectTransform.DOAnchorPosY(_rectTransform.anchoredPosition.y + _posY, _duration)
                .SetLoops(-1, LoopType.Yoyo)
                .SetUpdate(true);
        }

        private void StopAnimation()
        {
            _rectTransform.anchoredPosition = _initialAnchoredPosition;
            _animationTween?.Kill();
            _animationTween = null;
        }
    }
}