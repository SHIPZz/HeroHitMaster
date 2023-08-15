using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.ShopScrollRects.ShopScrollUnderlines
{
    public class ScrollNameUnderline : MonoBehaviour
    {
        [field: SerializeField] public Image Image { get; private set; }
        [field: SerializeField] public ScrollRectTypeId ScrollRectTypeId { get; private set; }
    }
}