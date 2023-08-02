using CodeBase.Services.Storages.ObjectParts;
using DG.Tweening;
using UnityEngine;

namespace CodeBase.Gameplay.ObjectBodyPart
{
    public class DestroyableObjectPartsActivator
    {
        private readonly DestroyableObjectPartStorage _destroyableObjectPartStorage;
        private bool _canActivate = true;

        public DestroyableObjectPartsActivator(DestroyableObjectPartStorage destroyableObjectPartStorage)
        {
            _destroyableObjectPartStorage = destroyableObjectPartStorage;
        }

        public void Disable() => 
            _canActivate = false;

        public void ActivateWithDisable(DestroyableObjectTypeId destroyableObjectTypeId)
        {
            if(!_canActivate)
                return;
            
            Debug.Log("activate");
            
            DestroyableObjectBodyPart destroyableObjectBodyPart = _destroyableObjectPartStorage.Get(destroyableObjectTypeId);
            destroyableObjectBodyPart.Enable();
            DOTween.Sequence().AppendInterval(5f).OnComplete(destroyableObjectBodyPart.Disable);
        }
    }
}