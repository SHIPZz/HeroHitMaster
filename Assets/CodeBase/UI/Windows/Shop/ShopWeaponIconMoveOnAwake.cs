using DG.Tweening;
using UnityEngine;

namespace CodeBase.UI.Windows.Shop
{
    public class ShopWeaponIconMoveOnAwake : MonoBehaviour
    {
        [SerializeField] private float _duration = 1f;

        private float _posY = 7.5f;
        private RectTransform _rectTransform;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            _rectTransform.DOAnchorPosY(_rectTransform.anchoredPosition.y + _posY, _duration)
                .SetLoops(-1, LoopType.Yoyo);
        }

        private void OnDisable()
        {
            transform.DOKill();
        }
    }
}