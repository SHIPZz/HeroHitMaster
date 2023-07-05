using AmazingAssets.AdvancedDissolve;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace Gameplay.Character.Enemy
{
    public class EnemyView : MonoBehaviour, IViewChangeable
    {
        private const float TargetValue = 1f;
        
        
        private SkinnedMeshRenderer _skinnedMeshRenderer;
        private Material _material;

        [Inject]
        private void Construct(SkinnedMeshRenderer skinnedMeshRenderer)
        {
            _skinnedMeshRenderer = skinnedMeshRenderer;
        }
        
        public void SetMaterial(Material material)
        {
            _material = material;
        }

        public void ShowDeath()
        {
            _skinnedMeshRenderer.material = _material;
            DOTween.To(() => 0, x => 
                _skinnedMeshRenderer.material.SetFloat(AdvancedDissolveProperties.Cutout.Standard._ids[0].clip,x), 
                1f, 1.5f);
        }
    }
}