using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace CodeBase.Traps.Flame
{
    public class FlameTrap : Trap
    {
        private const float DisableInterval = 1.5f;

        [SerializeField] private List<ParticleSystem> _flameEffects;

        protected override void Awake()
        {
            base.Awake();
            Collider.enabled = false;
            
        }

        public override void Activate()
        {
            _flameEffects.ForEach(x => x.Play());
            Collider.enabled = true;
            
            AutoDisable();
        }

        private void AutoDisable()
        {
            DOTween.Sequence()
                .AppendInterval(DisableInterval)
                .OnComplete(() =>
                {
                    Collider.enabled = false;
                    _flameEffects.ForEach(x => x.Stop());
                });
        }
    }
}