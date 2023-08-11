using System.Collections.Generic;
using CodeBase.Enums;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace CodeBase.Services.Providers
{
    public class MaterialProvider : SerializedMonoBehaviour, IProvider<MaterialTypeId, Material>
    {
        [OdinSerialize] private Dictionary<MaterialTypeId, Material> _materials;

        public Material Get(MaterialTypeId id) => 
            _materials[id];
    }
}