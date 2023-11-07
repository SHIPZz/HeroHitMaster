using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace CodeBase.Traps.Flame
{
    public class FlameTrap : Trap
    {
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
                .AppendInterval(DisableDelay)
                .OnComplete(() =>
                {
                    Collider.enabled = false;
                    _flameEffects.ForEach(x => x.Stop());
                });
        }
    }
}