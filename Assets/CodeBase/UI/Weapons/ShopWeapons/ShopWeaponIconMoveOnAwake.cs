using System;
using DG.Tweening;
using UnityEngine;

namespace CodeBase.UI.Weapons.ShopWeapons
{
    public class ShopWeaponIconMoveOnAwake : MonoBehaviour
    {
        [SerializeField] private float _posY;
        [SerializeField] private float _duration = 1f;
        
        private RectTransform _rectTransform;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            _rectTransform.DOAnchorPosY( _rectTransform.anchoredPosition.y + _posY, _duration).SetLoops(-1, LoopType.Yoyo);
        }
    }
}