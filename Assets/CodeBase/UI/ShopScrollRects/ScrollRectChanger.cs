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
        private bool _canChange = true;
        private IProvider<ScrollRectTypeId, List<Image>> _scrollImages;

        public event Action<bool> Changed;

        public ScrollRectChanger(IProvider<ScrollRectTypeId, List<Image>> scrollImages)
        {
            _scrollImages = scrollImages;
            _lastScrollRect = ScrollRectTypeId.Common;
        }

        public void Change(ScrollRectTypeId scrollRectTypeId)
        {
            if (scrollRectTypeId == _lastScrollRect || !_canChange)
                return;

            _canChange = false;
            Changed?.Invoke(true);
            _scrollImages.Get(_lastScrollRect).ForEach(x => x.transform.DOScale(0, 0f));
            _scrollImages.Get(scrollRectTypeId).ForEach(x => x.transform.DOScale(1, 0.5f)
                .SetEase(Ease.Linear)
                .OnComplete(() =>
                {
                    _canChange = true;
                    Changed?.Invoke(false);
                }));
            _lastScrollRect = scrollRectTypeId;
        }
    }
}