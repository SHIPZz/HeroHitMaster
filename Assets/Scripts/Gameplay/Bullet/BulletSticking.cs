using DG.Tweening;
using UnityEngine;
using Zenject;

namespace Gameplay.Bullet
{
    public class BulletSticking : ITickable
    {
        private IBullet _bullet;
        private Vector3 _stickTarget;

        public void Init(IBullet bullet, Vector3 stickTarget)
        {
            _stickTarget = stickTarget;
            _bullet = bullet;
        }

        public void Tick()
        {
            if (_bullet != null && _stickTarget != Vector3.zero)
            {
                _bullet.GameObject.transform.position = _stickTarget;
                DOTween.Sequence().AppendInterval(2f).OnComplete(() => _stickTarget = Vector3.zero);
            }
        }
    }
}