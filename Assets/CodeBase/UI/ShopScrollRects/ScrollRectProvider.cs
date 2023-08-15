using System.Collections.Generic;
using CodeBase.Services.Providers;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.ShopScrollRects
{
    public class ScrollRectProvider : SerializedMonoBehaviour, IProvider<Dictionary<ScrollRectTypeId, ScrollRect>>,
        IProvider<List<ScrollRectView>>
    {
        [OdinSerialize] private Dictionary<ScrollRectTypeId, ScrollRect> _scrollRects;
        [SerializeField] private List<ScrollRectView> _scrollRectViews;

        public Dictionary<ScrollRectTypeId, ScrollRect> Get() =>
            _scrollRects;

        public void Set(Dictionary<ScrollRectTypeId, ScrollRect> t) =>
            _scrollRects = t;

        public void Set(List<ScrollRectView> t) =>
            _scrollRectViews = t;

        List<ScrollRectView> IProvider<List<ScrollRectView>>.Get() =>
            _scrollRectViews;
    }
}