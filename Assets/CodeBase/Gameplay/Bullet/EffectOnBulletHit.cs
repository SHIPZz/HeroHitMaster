using CodeBase.Gameplay.Collision;
using CodeBase.Services.Data;
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

        [Inject]
        private void Construct(BulletEffectStorage bulletEffectStorage) => 
            _effect = bulletEffectStorage.Get(GetComponent<IBullet>().BulletTypeId);

        private void Awake() => 
            _triggerObserver = GetComponent<TriggerObserver>();

        private void OnEnable() =>
            _triggerObserver.Entered += Play;

        private void OnDisable() =>
            _triggerObserver.Entered -= Play;

        private void Play(Collider obj)
        {
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