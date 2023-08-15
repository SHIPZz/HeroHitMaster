using System.Collections.Generic;
using CodeBase.Services.Providers;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

namespace CodeBase.UI.ShopScrollRects.ShopScrollUnderlines
{
    public class ScrollNameUnderlinesProvider : SerializedMonoBehaviour, IProvider<ScrollRectTypeId, ScrollNameUnderline>
    {
        [OdinSerialize] private Dictionary<ScrollRectTypeId, ScrollNameUnderline> _scrollNameUnderlines;

        public ScrollNameUnderline Get(ScrollRectTypeId id) => 
            _scrollNameUnderlines[id];
    }
}