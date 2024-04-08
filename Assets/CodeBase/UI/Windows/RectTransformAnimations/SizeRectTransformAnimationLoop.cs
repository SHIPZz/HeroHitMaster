using System.Collections;
using UnityEngine;

namespace CodeBase.UI.Windows.RectTransformAnimations
{
    public class SizeRectTransformAnimationLoop : MonoBehaviour
    {
        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private Vector2 _targetSize;
        [SerializeField] private float _animationDuration = 1f;

        private Vector2 _initialSize;
        private Coroutine _currentAnimation;

        private void Start()
        {
            if (_rectTransform == null)
                _rectTransform = GetComponent<RectTransform>();

            _initialSize = _rectTransform.sizeDelta;

            StartAnimation();
        }

        private void StartAnimation()
        {
            if (_currentAnimation != null)
                StopCoroutine(_currentAnimation);

            _currentAnimation = StartCoroutine(AnimateSize());
        }

        private IEnumerator AnimateSize()
        {
            while (true)
            {
                yield return ChangeSize(_targetSize, _animationDuration);
                yield return ChangeSize(_initialSize, _animationDuration);
            }
        }

        private IEnumerator ChangeSize(Vector2 targetSize, float duration)
        {
            Vector2 startSize = _rectTransform.sizeDelta;
            float timer = 0f;

            while (timer < duration)
            {
                timer += Time.deltaTime;
                _rectTransform.sizeDelta = Vector2.Lerp(startSize, targetSize, timer / duration);
                yield return null;
            }

            _rectTransform.sizeDelta = targetSize;
        }
    }
}