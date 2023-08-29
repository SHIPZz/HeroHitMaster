using CodeBase.Services.Factories;
using UnityEngine;
using Zenject;

namespace CodeBase.Gameplay.ObjectBodyPart
{
    public class DestroyableObjectBodyPartPositionSetter : MonoBehaviour
    {
        private ObjectPartFactory _objectPartFactory;

        [Inject]
        public void Construct(ObjectPartFactory objectPartFactory)
        {
            _objectPartFactory = objectPartFactory;
        }

        public void Set(DestroyableObjectTypeId destroyableObjectTypeId, DestroyableObject destroyableObject)
        {
            DestroyableObjectBodyPart destroyableParts = _objectPartFactory.CreateBy(destroyableObjectTypeId,destroyableObject.transform.position);
            destroyableParts.Enable();
        }
    }
}