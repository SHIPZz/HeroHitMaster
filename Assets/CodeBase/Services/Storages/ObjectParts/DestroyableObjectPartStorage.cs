using System.Collections.Generic;
using CodeBase.Gameplay.ObjectBodyPart;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

namespace CodeBase.Services.Storages.ObjectParts
{
    public class DestroyableObjectPartStorage : SerializedMonoBehaviour
    {
        [OdinSerialize] private Dictionary<DestroyableObjectTypeId, DestroyableObjectBodyPart> _objectBodyParts;
        
        public DestroyableObjectBodyPart Get(DestroyableObjectTypeId destroyableObjectTypeId) => 
            _objectBodyParts[destroyableObjectTypeId];

        public List<DestroyableObjectBodyPart> GetAll()
        {
            var objectBodyParts = new List<DestroyableObjectBodyPart>();

            foreach (var enemyBodyPartView in _objectBodyParts.Values)
            {
                objectBodyParts.Add(enemyBodyPartView);
            }

            return objectBodyParts;
        }
    }
}