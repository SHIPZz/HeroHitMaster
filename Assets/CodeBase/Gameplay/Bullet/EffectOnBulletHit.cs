using System.Collections.Generic;
using CodeBase.Constants;
using CodeBase.Gameplay.Collision;
using CodeBase.Services.Storages.Bullet;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace CodeBase.Gameplay.Bullet
{
    public class EffectOnBulletHit : MonoBehaviour
    {
        private TriggerObserver _triggerObserver;

        private ParticleSystem _effect;

        private readonly List<int> _layersToBlockEffect = new()
        {
            LayerId.Floor,
            LayerId.HardCube,
            LayerId.Wall,
            LayerId.Water,
        };

        [Inject]
        private void Construct(BulletEffectStorage bulletEffectStorage) => 
            _effect = bulletEffectStorage.Get(GetComponent<Bullet>().WeaponTypeId);

        private void Awake() => 
            _triggerObserver = GetComponent<TriggerObserver>();

        private void OnEnable() =>
            _triggerObserver.CollisionEntered += Play;

        private void OnDisable() =>
            _triggerObserver.CollisionEntered -= Play;

        private void Play(UnityEngine.Collision collision)
        {
            if(_layersToBlockEffect.Contains(collision.gameObject.layer))
                return;
            
            _effect.transform.position = transform.position;
            _effect.Play();
            
            DOTween.Sequence().AppendInterval(1f).OnComplete(() =>
            {
                gameObject.SetActive(false);
                _effect.Stop();
            });
        }
    }
}