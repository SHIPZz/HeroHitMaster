using System;
using Enums;

namespace Gameplay.WeaponSelection
{
    public interface ISelectorView
    {
        SelectorViewTypeId SelectorViewTypeId { get; }
        public event Action LeftArrowClicked;
        public event Action RightArrowClicked;
        public event Action ApplyButtonClicked;

        void Show(Enum @enum);
    }
}