using System;
using System.Collections.Generic;
using CodeBase.Services.Providers;
using DG.Tweening;
using UnityEngine.UI;

namespace CodeBase.UI.ShopScrollRects
{
    public class ScrollRectChanger
    {
        private ScrollRectTypeId _lastScrollRect;
        private IProvider<ScrollRectTypeId, List<Image>> _scrollImagesProvider;
        private IProvider<ScrollRectTypeId, ScrollRect> _scrollRectsProvider;

        public event Action<bool> Changed;

        public ScrollRectChanger(IProvider<ScrollRectTypeId, List<Image>> scrollImagesProvider,
            IProvider<ScrollRectTypeId, ScrollRect> scrollRectsProvider)
        {
            _scrollRectsProvider = scrollRectsProvider;
            _scrollImagesProvider = scrollImagesProvider;
            _lastScrollRect = ScrollRectTypeId.Common;
        }

        public void Change(ScrollRectTypeId scrollRectTypeId)
        {
            if (scrollRectTypeId == _lastScrollRect)
                return;

            Changed?.Invoke(true);

            DisableAll(_lastScrollRect);
            EnableTargetScroll(scrollRectTypeId);
            _lastScrollRect = scrollRectTypeId;
        }

        private void EnableTargetScroll(ScrollRectTypeId scrollRectTypeId)
        {
            _scrollRectsProvider.Get(scrollRectTypeId).gameObject.SetActive(true);
            _scrollImagesProvider
                .Get(scrollRectTypeId)
                .ForEach(Enable);
        }

        private void Enable(Image image) =>
            image.transform
                .DOScale(1, 0.5f)
                .SetEase(Ease.Linear)
                .OnComplete(() =>
                {
                    Changed?.Invoke(false);
                });

        private void DisableAll(ScrollRectTypeId scrollRectTypeId) =>
            _scrollImagesProvider
                .Get(scrollRectTypeId)
                .ForEach(x => x.transform.DOScale(0, 0f)
                    .OnComplete(() => _scrollRectsProvider.Get(scrollRectTypeId).gameObject.SetActive(false)));
    }
}