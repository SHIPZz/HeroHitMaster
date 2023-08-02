using System.Collections.Generic;
using CodeBase.Gameplay.ObjectBodyPart;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

namespace CodeBase.Services.Storages.ObjectParts
{
    public class DestroyableObjectStorage : SerializedMonoBehaviour
    {
        [OdinSerialize] private Dictionary<DestroyableObjectTypeId, DestroyableObject> _destroyableObjects;

        public DestroyableObject GetBy(DestroyableObjectTypeId destroyableObjectTypeId) =>
            _destroyableObjects[destroyableObjectTypeId];

        public List<DestroyableObject> GetAll()
        {
            var destroyableObjects = new List<DestroyableObject>();

            foreach (var destroyableObject in _destroyableObjects.Values)
            {
                destroyableObjects.Add(destroyableObject);
            }

            return destroyableObjects;
        }
    }
}