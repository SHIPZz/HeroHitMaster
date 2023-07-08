using System;
using Gameplay.Character;
using UnityEngine;
using Zenject;

namespace Gameplay.MaterialChanger
{
    public class SkinnedMeshVisibilityHandler : IInitializable, IDisposable
    {
        private readonly IHealth _health;
        private readonly IMaterialChanger _skinnedMaterialChanger;
        private readonly SkinnedMeshRenderer _skinnedMeshRenderer;
        private bool _isMaterialChanged;

        public SkinnedMeshVisibilityHandler(IHealth health, IMaterialChanger skinnedMaterialChanger,
            SkinnedMeshRenderer skinnedMeshRenderer)
        {
            _health = health;
            _skinnedMaterialChanger = skinnedMaterialChanger;
            _skinnedMeshRenderer = skinnedMeshRenderer;
        }

        public void Initialize()
        {
            _health.ValueZeroReached += Disable;
            _skinnedMaterialChanger.Changed += MaterialChanged;
        }

        public void Dispose()
        {
            _health.ValueZeroReached -= Disable;
            _skinnedMaterialChanger.Changed -= MaterialChanged;
        }

        private void MaterialChanged() => 
            _isMaterialChanged = true;

        private void Disable()
        {
            if(_isMaterialChanged)
                return;
            
            _skinnedMeshRenderer.enabled = false;
        }
    }
}