using System;
using AmazingAssets.AdvancedDissolve;
using DG.Tweening;
using UnityEngine;

namespace CodeBase.Gameplay.MaterialChanger
{
    [RequireComponent(typeof(MeshRenderer))]
    public class MeshMaterialChanger : MonoBehaviour, IMaterialChanger
    {
        [SerializeField] private float _duration = 1.5f;
        [SerializeField] private float _targetValue = 1f;
        [SerializeField] private bool _needDisable = true;

        private MeshRenderer _meshRenderer;

        public event Action StartedChanged;
        public event Action Completed;
        public bool IsChanging { get; private set; }

        private void Awake() =>
            _meshRenderer = GetComponent<MeshRenderer>();

        public void Change(Material material)
        {
            _meshRenderer.material = material;
            GetComponent<Collider>().enabled = false;
            StartedChanged?.Invoke();
            IsChanging = true;

            DOTween.To(() => 0, x =>
                    _meshRenderer.material.SetFloat(AdvancedDissolveProperties.Cutout.Standard._ids[0].clip, x),
                _targetValue, _duration).OnComplete(OnMaterialChanged);
        }

        private void OnMaterialChanged()
        {
            Completed?.Invoke();

            // if (_needDisable)
            //     gameObject.SetActive(false);
        }
    }
}