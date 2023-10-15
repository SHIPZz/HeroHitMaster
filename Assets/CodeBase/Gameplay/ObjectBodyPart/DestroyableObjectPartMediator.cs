using UnityEngine;

namespace CodeBase.Gameplay.ObjectBodyPart
{
    public class DestroyableObjectPartMediator : MonoBehaviour
    {
        [SerializeField] private DestroyableObjectBodyPartPositionSetter _destroyableObjectBodyPartPositionSetter;
        [SerializeField] private DestroyableObject _destroyableObject;
        
        private void OnEnable() => 
            _destroyableObject.Destroyed += Set;

        private void OnDisable() => 
            _destroyableObject.Destroyed -= Set;

        private void Set(DestroyableObjectTypeId destroyableObjectTypeId) => 
            _destroyableObjectBodyPartPositionSetter.Set(destroyableObjectTypeId, _destroyableObject);
    }
}