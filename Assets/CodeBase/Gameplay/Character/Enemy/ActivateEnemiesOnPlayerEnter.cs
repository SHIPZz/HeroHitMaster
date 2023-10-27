using System;
using System.Collections.Generic;
using CodeBase.Gameplay.Character.Players;
using CodeBase.Gameplay.Character.Players.Shoot;
using CodeBase.Gameplay.Spawners;
using CodeBase.Services.Providers;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace CodeBase.Gameplay.Character.Enemy
{
    public class ActivateEnemiesOnPlayerEnter : MonoBehaviour
    {
        [SerializeField] private List<EnemySpawner> _enemySpawners;
        [SerializeField] private float _delayActivation = 0.5f;

        private List<Enemy> _enemies = new();
        private AggroZone _aggroZone;
        private PlayerProvider _playerProvider;
        private ShootingOnAnimationEvent _shootingOnAnimationEvent;
        private bool _canMove;

        public event Action<Enemy> Activated;

        private bool _activated = false;

        [Inject]
        private void Construct(IProvider<PlayerProvider> provider)
        {
            _playerProvider = provider.Get();
            _playerProvider.Changed += SetPlayer;
        }

        private void Awake() =>
            _aggroZone = GetComponent<AggroZone>();

        private void OnEnable()
        {
            _enemySpawners.ForEach(x => x.Spawned += FillList);
            _aggroZone.PlayerEntered += Activate;
        }

        private void OnDisable()
        {
            _enemySpawners.ForEach(x => x.Spawned -= FillList);
            _shootingOnAnimationEvent.Shooted -= SetMove;
            _aggroZone.PlayerEntered -= Activate;
        }

        private void SetPlayer(Player player)
        {
            _shootingOnAnimationEvent = player.GetComponent<ShootingOnAnimationEvent>();
            _shootingOnAnimationEvent.Shooted += SetMove;
        }

        private void SetMove() =>
            _canMove = true;

        private async void Activate(Player obj)
        {
            if (_activated)
                return;

            await UniTask.WaitForSeconds(_delayActivation);

            foreach (Enemy enemy in _enemies)
            {
                enemy.gameObject.SetActive(true);
                enemy.gameObject.transform.DOScale(enemy.InitialScale, 0.5f);
                Activated?.Invoke(enemy);
                TryActivateMovement(enemy);
            }

            _activated = true;
        }

        private async void TryActivateMovement(Enemy enemy)
        {
            while (!_canMove)
            {
                await UniTask.Yield();
            }

            await UniTask.WaitForSeconds(0.1f);
            var enemyFollower = enemy.GetComponent<EnemyFollower>();
            enemyFollower.Unblock();
        }

        private void FillList(Enemy enemy) =>
            _enemies.Add(enemy);
    }
}