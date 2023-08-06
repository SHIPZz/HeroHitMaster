using System;
using AmazingAssets.AdvancedDissolve;
using CodeBase.Constants;
using CodeBase.Enums;
using CodeBase.Gameplay.Character.Enemy;
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

        public event Action Changed;

        [Inject]
        private void Construct(SkinnedMeshRenderer skinnedMeshRenderer)
        {
            _skinnedMeshRenderer = skinnedMeshRenderer;
        }

        public void Change(Material material)
        {
            SetStartValues(material);
            GetComponent<Collider>().enabled = false;

            DOTween.To(() => 0, x =>
                    _skinnedMeshRenderer.material.SetFloat(AdvancedDissolveProperties.Cutout.Standard._ids[0].clip, x),
                _targetValue, _duration).OnComplete(() => { Destroy(gameObject); });
            
            
            Changed?.Invoke();
        }

        private void SetStartValues(Material material)
        {
            _skinnedMeshRenderer.material = material;
            _skinnedMeshRenderer.material.SetFloat(AdvancedDissolveProperties.Cutout.Standard._ids[0].clip, 0f);
        }
    }
}