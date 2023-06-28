﻿using DG.Tweening;
using Gameplay.Bullet;
using UnityEngine;

namespace Gameplay.Web
{
    public class WebMovement : IBulletMovement
    {
        public void Move(Vector3 target, IBullet bullet, Vector3 startPosition, float duration)
        {
            bullet.GameObject.transform.position = startPosition;
            bullet.GameObject.transform.DOMove(target, duration);
        }
    }
}