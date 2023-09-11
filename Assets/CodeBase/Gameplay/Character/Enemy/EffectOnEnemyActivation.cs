﻿using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CodeBase.Gameplay.Character.Enemy
{
    public class EffectOnEnemyActivation : MonoBehaviour
    {
        [SerializeField] private List<ParticleSystem> _effects;
        
        private ActivateEnemiesOnPlayerEnter _activateEnemy;

        private void Awake()
        {
            _activateEnemy = GetComponent<ActivateEnemiesOnPlayerEnter>();
        }

        private void OnEnable()
        {
            _activateEnemy.Activated += Play;
        }

        private void OnDisable()
        {
            _activateEnemy.Activated -= Play;
        }

        private void Play(Enemy enemy)
        {
            var randomId = Random.Range(0, _effects.Count - 1);
            ParticleSystem randomEffect = _effects[randomId];
            randomEffect.gameObject.transform.position = enemy.transform.position + Vector3.up * 2;
            randomEffect.Play();
        }
    }
}