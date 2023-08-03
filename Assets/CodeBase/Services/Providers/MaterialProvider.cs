using System.Collections.Generic;
using CodeBase.Enums;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace CodeBase.Services.Providers
{
    public class MaterialProvider : SerializedMonoBehaviour
    {
        [OdinSerialize] public Dictionary<MaterialTypeId, Material> Materials { get; private set; }
    }
}