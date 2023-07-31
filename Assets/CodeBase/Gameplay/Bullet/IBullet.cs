﻿using CodeBase.Enums;
using UnityEngine;

namespace CodeBase.Gameplay.Bullet
{
    public interface IBullet
    {
        GameObject GameObject { get; }
        Rigidbody Rigidbody { get; }

        BulletTypeId BulletTypeId { get; }
        void DoDamage(Collider other);
    }
}