using System;
using CodeBase.Enums;

namespace CodeBase.Gameplay.Weapons.Price
{
    [Serializable]
    public struct Price
    {
        public int Value;
        public PriceTypeId PriceTypeId;
        public int AdQuantity;
    }
}