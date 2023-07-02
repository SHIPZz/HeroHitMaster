using System;
using UnityEngine;

namespace Gameplay.Character.Enemy
{
    public class Enemy : MonoBehaviour, IDamageable
    {
        private readonly Health _health = new Health(100);
        private U10PS_DissolveOverTime _dissolveOverTime;

        private void Awake()
        {
            _dissolveOverTime = GetComponent<U10PS_DissolveOverTime>();
        }

        private void OnEnable()
        {
            _health.ValueZeroReached += () =>
            {
                GetComponent<Collider>().enabled = false;
                GetComponent<U10PS_DissolveOverTime>().enabled = true;
                Destroy(gameObject,1.5f);
            };
        }

        public void TakeDamage(int value)
        {
            _health.TakeDamage(value);
        }
    }
}