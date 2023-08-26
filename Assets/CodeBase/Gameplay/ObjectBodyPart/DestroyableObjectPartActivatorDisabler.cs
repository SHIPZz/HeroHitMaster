using System;
using CodeBase.Gameplay.MaterialChanger;
using Zenject;

namespace CodeBase.Gameplay.ObjectBodyPart
{
    public class DestroyableObjectPartActivatorDisabler : IInitializable, IDisposable
    {
        private readonly IMaterialChanger _materialChanger;
        private readonly DestroyableObjectPartsActivator _destroyableObjectPartsActivator;

        public DestroyableObjectPartActivatorDisabler(IMaterialChanger materialChanger,
            DestroyableObjectPartsActivator destroyableObjectPartsActivator)
        {
            _destroyableObjectPartsActivator = destroyableObjectPartsActivator;
            _materialChanger = materialChanger;
        }
        
        public void Initialize() =>
            _materialChanger.StartedChanged += _destroyableObjectPartsActivator.Disable;

        public void Dispose() =>
            _materialChanger.StartedChanged -= _destroyableObjectPartsActivator.Disable;
    }
}