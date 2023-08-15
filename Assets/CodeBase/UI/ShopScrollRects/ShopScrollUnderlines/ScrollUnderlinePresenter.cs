using System;
using System.Collections.Generic;
using CodeBase.Services.Providers;
using Zenject;

namespace CodeBase.UI.ShopScrollRects.ShopScrollUnderlines
{
    public class ScrollUnderlinePresenter : IInitializable, IDisposable
    {
        private readonly ScrollUnderlineChanger _scrollUnderlineChanger;
        private readonly List<ScrollRectView> _scrollViews;

        public ScrollUnderlinePresenter(IProvider<List<ScrollRectView>> provider, ScrollUnderlineChanger scrollUnderlineChanger)
        {
            _scrollViews = provider.Get();
            _scrollUnderlineChanger = scrollUnderlineChanger;
        }

        public void Initialize()
        {
            _scrollViews.ForEach(x => x.Opened += _scrollUnderlineChanger.Change);
        }

        public void Dispose()
        {
            _scrollViews.ForEach(x => x.Opened -= _scrollUnderlineChanger.Change);
        }
    }
}