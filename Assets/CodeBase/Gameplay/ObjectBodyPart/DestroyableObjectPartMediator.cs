using UnityEngine;

namespace CodeBase.Gameplay.ObjectBodyPart
{
    public class DestroyableObjectPartMediator : MonoBehaviour
    {
        [SerializeField] private DestroyableObjectBodyPartPositionSetter _destroyableObjectBodyPartPositionSetter;
        [SerializeField] private DestroyableObject destroyableObject;
        
        private void OnEnable()
        {
            destroyableObject.Destroyed += Set;
        }

        private void OnDisable()
        {
            destroyableObject.Destroyed -= Set;
        }

        private void Set(DestroyableObjectTypeId destroyableObjectTypeId) => 
            _destroyableObjectBodyPartPositionSetter.Set(destroyableObjectTypeId, destroyableObject);
    }
}