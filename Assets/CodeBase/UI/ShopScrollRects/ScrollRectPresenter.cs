using System;
using System.Collections.Generic;
using CodeBase.Services.Providers;
using Zenject;

namespace CodeBase.UI.ShopScrollRects
{
    public class ScrollRectPresenter : IInitializable, IDisposable
    {
        private readonly List<ScrollRectView> _scrollRects;
        private readonly ScrollRectChanger _scrollRectChanger;

        public ScrollRectPresenter(IProvider<List<ScrollRectView>> scrollRects,
            ScrollRectChanger scrollRectChanger)
        {
            _scrollRectChanger = scrollRectChanger;
            _scrollRects = scrollRects.Get();
        }

        public void Initialize()
        {
            _scrollRects.ForEach(x => x.Opened += _scrollRectChanger.Change);
        }

        public void Dispose()
        {
            _scrollRects.ForEach(x => x.Opened -= _scrollRectChanger.Change);
        }
    }
}