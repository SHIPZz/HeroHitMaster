using System;
using System.Collections.Generic;
using CodeBase.Services.Storages.ObjectParts;
using Zenject;

namespace CodeBase.Gameplay.ObjectBodyPart
{
    public class DestroyableObjectPartMediator : IInitializable, IDisposable
    {
        private DestroyableObjectStorage _destroyableObjectStorage;
        private readonly List<DestroyableObject> _destroyableObjects;
        private readonly DestroyableObjectPartsActivator _destroyableObjectPartsActivator;
        private readonly DestroyableObjectBodyPartPositionSetter _destroyableObjectBodyPartPositionSetter;

        public DestroyableObjectPartMediator(DestroyableObjectStorage destroyableObjectStorage, 
            DestroyableObjectPartsActivator destroyableObjectPartsActivator, 
            DestroyableObjectBodyPartPositionSetter destroyableObjectBodyPartPositionSetter)
        {
            _destroyableObjectBodyPartPositionSetter = destroyableObjectBodyPartPositionSetter;
            _destroyableObjectPartsActivator = destroyableObjectPartsActivator;
            _destroyableObjects = destroyableObjectStorage.GetAll();
        }

        public void Initialize()
        {
            _destroyableObjects.ForEach(x =>
            {
                x.Destroyed += _destroyableObjectPartsActivator.ActivateWithDisable;
                x.Destroyed += _destroyableObjectBodyPartPositionSetter.Set;
            });
        }

        public void Dispose()
        {
            _destroyableObjects.ForEach(x =>
            {
                x.Destroyed -= _destroyableObjectPartsActivator.ActivateWithDisable;
                x.Destroyed -= _destroyableObjectBodyPartPositionSetter.Set;
            });
        }
    }
}