using DG.Tweening;
using UnityEngine;

namespace Gameplay.Web
{
    public class BulletMovement
    {
        public void Move(Vector3 target, IBullet bullet, Vector3 startPosition)
        {
            bullet.GameObject.transform.position = startPosition;
            bullet.GameObject.transform.DOMove(target, 0.2f);
        }
    }
}