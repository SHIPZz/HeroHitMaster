using System;
using UnityEngine;

namespace CodeBase.Gameplay.ObjectBodyPart
{
    [RequireComponent(typeof(DestroyableObjectBodyPartPositionSetter), typeof(DestroyableObject))]
    public class DestroyableObjectPartMediator : MonoBehaviour
    {
        private DestroyableObjectBodyPartPositionSetter _destroyableObjectBodyPartPositionSetter;
        private DestroyableObject _destroyableObject;

        private void Awake()
        {
            _destroyableObjectBodyPartPositionSetter = GetComponent<DestroyableObjectBodyPartPositionSetter>();
            _destroyableObject = GetComponent<DestroyableObject>();
        }

        private void OnEnable() =>
            _destroyableObject.Destroyed += Set;

        private void OnDisable() =>
            _destroyableObject.Destroyed -= Set;

        private void Set(DestroyableObjectTypeId destroyableObjectTypeId) =>
            _destroyableObjectBodyPartPositionSetter.Set(destroyableObjectTypeId, _destroyableObject);
    }
}