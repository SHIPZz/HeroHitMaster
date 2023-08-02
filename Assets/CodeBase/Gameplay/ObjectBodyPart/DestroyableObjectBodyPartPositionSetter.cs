using CodeBase.Services.Storages.ObjectParts;

namespace CodeBase.Gameplay.ObjectBodyPart
{
    public class DestroyableObjectBodyPartPositionSetter
    {
        private readonly DestroyableObjectPartStorage _destroyableObjectPartStorage;
        private readonly DestroyableObjectStorage _destroyableObjectStorage;

        public DestroyableObjectBodyPartPositionSetter(DestroyableObjectPartStorage destroyableObjectPartStorage, 
            DestroyableObjectStorage destroyableObjectStorage)
        {
            _destroyableObjectPartStorage = destroyableObjectPartStorage;
            _destroyableObjectStorage = destroyableObjectStorage;
        }

        public void Set(DestroyableObjectTypeId destroyableObjectTypeId)
        {
            DestroyableObjectBodyPart destroyableParts = _destroyableObjectPartStorage.Get(destroyableObjectTypeId);
            DestroyableObject destroyableObject = _destroyableObjectStorage.GetBy(destroyableObjectTypeId);

            destroyableParts.transform.position = destroyableObject.transform.position;
        }
    }
}