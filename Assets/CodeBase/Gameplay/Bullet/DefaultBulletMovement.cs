using System.Collections;
using CodeBase.Services.Data;
using UnityEngine;
using Zenject;

namespace CodeBase.Gameplay.Bullet
{
    public class DefaultBulletMovement : BulletMovement
    {
        private Vector3 _currentVelocity;
        private Coroutine _moveCoroutine;


    }
}