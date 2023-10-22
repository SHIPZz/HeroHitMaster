using System;
using System.Collections.Generic;
using CodeBase.Enums;

namespace CodeBase.Services.SaveSystems.Data
{
    [Serializable]
    public class TranslatedWeaponNameData
    {
        public Dictionary<WeaponTypeId, string> Names = new();
    }
}