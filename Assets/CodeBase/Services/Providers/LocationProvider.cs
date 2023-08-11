using System.Collections.Generic;
using CodeBase.Enums;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace CodeBase.Services.Providers
{
    public class LocationProvider : SerializedMonoBehaviour, IProvider<LocationTypeId, Transform>
    {
        [OdinSerialize] private Dictionary<LocationTypeId, Transform> _values;

        public Transform Get(LocationTypeId id) =>
            _values[id];
    }
}