using System;
using AmazingAssets.AdvancedDissolve;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace CodeBase.Gameplay.MaterialChanger
{
    [RequireComponent(typeof(SkinnedMeshRenderer))]
    public class SkinnedMaterialChanger : MonoBehaviour, IMaterialChanger
    {
        [SerializeField] private float _duration = 3.5f;
        [SerializeField] private float _targetValue = 1f;

        private SkinnedMeshRenderer _skinnedMeshRenderer;

        public event Action StartedChanged;
        public event Action Completed;
        public bool IsChanging { get; private set; }

        private void Awake() =>
            _skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();

        public void Change(Material material)
        {
            if (IsChanging)
                return;
            
            SetStartValues(material);
            GetComponent<Collider>().enabled = false;

            IsChanging = true;

            DOTween.To(() => 0, x =>
                    _skinnedMeshRenderer.material.SetFloat(AdvancedDissolveProperties.Cutout.Standard._ids[0].clip, x),
                _targetValue, _duration).OnComplete(() =>
            {
                Completed?.Invoke();
                gameObject.SetActive(false);
            });


            StartedChanged?.Invoke();
        }

        private void SetStartValues(Material material)
        {
            _skinnedMeshRenderer.material = material;
            _skinnedMeshRenderer.material.SetFloat(AdvancedDissolveProperties.Cutout.Standard._ids[0].clip, 0f);
        }
    }
}