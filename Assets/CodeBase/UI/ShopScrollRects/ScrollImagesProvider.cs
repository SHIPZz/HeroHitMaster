using System.Collections.Generic;
using CodeBase.Services.Providers;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine.UI;

namespace CodeBase.UI.ShopScrollRects
{
    public class ScrollImagesProvider : SerializedMonoBehaviour, IProvider<ScrollRectTypeId, List<Image>>
    {
        [OdinSerialize] private Dictionary<ScrollRectTypeId, List<Image>> _images;

        public List<Image> Get(ScrollRectTypeId id) => 
            _images[id];
    }
}