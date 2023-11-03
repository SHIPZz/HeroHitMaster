using CodeBase.Services.Providers;
using DG.Tweening;

namespace CodeBase.UI.ShopScrollRects.ShopScrollUnderlines
{
    public class ScrollUnderlineChanger
    {
        private readonly IProvider<ScrollRectTypeId, ScrollNameUnderline> _provider;
        private ScrollNameUnderline _lastUnderline;

        public ScrollUnderlineChanger(IProvider<ScrollRectTypeId, ScrollNameUnderline> provider)
        {
            _provider = provider;
            _lastUnderline = _provider.Get(ScrollRectTypeId.Common);
        }
        
        public void Change(ScrollRectTypeId id)
        {
            if(_lastUnderline == _provider.Get(id))
                return;
            
            _lastUnderline.Image.transform.DOScaleX(0, 0.3f).SetUpdate(true);
            ScrollNameUnderline targetUnderline = _provider.Get(id);
            targetUnderline.Image.transform.DOScaleX(1, 0.5f).SetUpdate(true);
            _lastUnderline = targetUnderline;
        }
    }
}