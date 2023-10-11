﻿using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CodeBase.UI.Weapons
{
    [RequireComponent(typeof(BoxCollider))]
    public class WeaponViewPointer : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private Vector2 _offset;

        private Vector2 _upPosition;
        private Vector2 _initialAnchorPosition;
        private RectTransform _rectTransform;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            _upPosition = _rectTransform.anchoredPosition + _offset;
            _initialAnchorPosition = _rectTransform.anchoredPosition;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            StopAllCoroutines(); 
            StartCoroutine(MoveToPosition(_upPosition));
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            StopAllCoroutines();
            StartCoroutine(MoveToPosition(_initialAnchorPosition));
        }

        private IEnumerator MoveToPosition(Vector2 targetPosition)
        {
            float duration = 0.3f;
            float elapsed = 0f;

            Vector2 startPosition = _rectTransform.anchoredPosition;

            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                float t = elapsed / duration;
                _rectTransform.anchoredPosition = Vector2.Lerp(startPosition, targetPosition, t);
                yield return null;
            }

            _rectTransform.anchoredPosition = targetPosition;
        }
    }
}