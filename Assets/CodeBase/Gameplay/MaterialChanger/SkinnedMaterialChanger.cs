using System;
using AmazingAssets.AdvancedDissolve;
using CodeBase.Enums;
using CodeBase.Gameplay.Character.Enemy;
using Constants;
using DG.Tweening;
using UnityEngine;
using Zenject;
using Random = System.Random;

namespace CodeBase.Gameplay.MaterialChanger
{
    public class SkinnedMaterialChanger : MonoBehaviour, IMaterialChanger
    {
        [SerializeField] private float _duration = 3.5f;
        [SerializeField] private float _targetValue = 1f;

        private SkinnedMeshRenderer _skinnedMeshRenderer;
        private EnemyDestroyOnDeath _enemyDestroyOnDeath;
        private MaterialProvider _materialProvider;
        private static Random _random = new();

        public event Action Changed;

        [Inject]
        private void Construct(SkinnedMeshRenderer skinnedMeshRenderer, EnemyDestroyOnDeath enemyDestroyOnDeath,MaterialProvider materialProvider)
        {
            _materialProvider = materialProvider;
            _enemyDestroyOnDeath = enemyDestroyOnDeath;
            _skinnedMeshRenderer = skinnedMeshRenderer;
        }

        public void Change(Material material)
        {
            // SetStartValues(material);

            var randomMaterial = _materialProvider.Materials[(MaterialTypeId)2];
            SetStartValues(randomMaterial);
            
            DOTween.To(() => 0, x =>
                    _skinnedMeshRenderer.material.SetFloat(AdvancedDissolveProperties.Cutout.Standard._ids[0].clip, x),
                _targetValue, _duration);

            _enemyDestroyOnDeath.Delay(DelayValues.DestroyEffectDelay);
            
            Changed?.Invoke();
        }

        private void SetStartValues(Material material)
        {
            _skinnedMeshRenderer.material = material;
            _skinnedMeshRenderer.material.SetFloat(AdvancedDissolveProperties.Cutout.Standard._ids[0].clip, 0f);
        }
    }
}