using System;
using AmazingAssets.AdvancedDissolve;
using DG.Tweening;
using Gameplay.Character.Enemy;
using UnityEngine;
using Zenject;

namespace Gameplay.MaterialChanger
{
    public class SkinnedMaterialChanger : MonoBehaviour, IMaterialChanger
    {
        private const float DestroyDelay = 4f;
        
        [SerializeField] private float _duration = 3.5f;
        [SerializeField] private float _targetValue = 1f;

        private SkinnedMeshRenderer _skinnedMeshRenderer;
        private EnemyDestroyOnDeath _enemyDestroyOnDeath;
        public event Action Changed;

        [Inject]
        private void Construct(SkinnedMeshRenderer skinnedMeshRenderer, EnemyDestroyOnDeath enemyDestroyOnDeath)
        {
            _enemyDestroyOnDeath = enemyDestroyOnDeath;
            _skinnedMeshRenderer = skinnedMeshRenderer;
        }

        public void Change(Material material)
        {
            _skinnedMeshRenderer.material = material;
            _skinnedMeshRenderer.material.SetFloat(AdvancedDissolveProperties.Cutout.Standard._ids[0].clip, 0f);
            DOTween.To(() => 0, x =>
                    _skinnedMeshRenderer.material.SetFloat(AdvancedDissolveProperties.Cutout.Standard._ids[0].clip, x),
                _targetValue, _duration);
            _enemyDestroyOnDeath.Delay(DestroyDelay);
            
            Changed?.Invoke();
        }
    }
}