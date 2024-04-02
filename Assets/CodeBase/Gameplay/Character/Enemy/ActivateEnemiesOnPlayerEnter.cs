using System.Collections.Generic;
using CodeBase.Enums;
using CodeBase.Gameplay.Character.Players;
using CodeBase.Gameplay.Character.Players.Shoot;
using CodeBase.Gameplay.EffectsData;
using CodeBase.Gameplay.Spawners;
using CodeBase.Services.Providers;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace CodeBase.Gameplay.Character.Enemy
{
    [RequireComponent(typeof(EffectOnEnemyActivation))]
    public class ActivateEnemiesOnPlayerEnter : MonoBehaviour
    {
        private const float DelayActivation = 1.5f;
        [SerializeField] private List<EnemySpawner> _enemySpawners;

        private List<Enemy> _enemies = new();
        private AggroZone _aggroZone;
        private PlayerProvider _playerProvider;
        private ShootingOnAnimationEvent _shootingOnAnimationEvent;
        private bool _canMove;
        private EffectOnEnemyActivation _effectOnEnemyActivation;


        private bool _activated = false;

        [Inject]
        private void Construct(IProvider<PlayerProvider> provider)
        {
            _playerProvider = provider.Get();
            _playerProvider.Changed += SetPlayer;
        }

        private void Awake()
        {
            _aggroZone = GetComponent<AggroZone>();
            _effectOnEnemyActivation = GetComponent<EffectOnEnemyActivation>();
        }

        private void OnEnable()
        {
            _enemySpawners.ForEach(x => x.Spawned += FillList);
            _aggroZone.PlayerEntered += Activate;
        }

        private void OnDisable()
        {
            _enemySpawners.ForEach(x => x.Spawned -= FillList);
            _shootingOnAnimationEvent.Shot -= SetMove;
            _aggroZone.PlayerEntered -= Activate;
        }

        private void SetPlayer(Player player)
        {
            _shootingOnAnimationEvent = player.GetComponent<ShootingOnAnimationEvent>();
            _shootingOnAnimationEvent.Shot += SetMove;
        }

        private void SetMove() =>
            _canMove = true;

        private void Activate(Player obj)
        {
            if (_activated)
                return;

            foreach (Enemy enemy in _enemies)
            {
                enemy.gameObject.SetActive(true);
                enemy.gameObject.transform.DOScale(enemy.InitialScale, 0.5f)
                    .OnComplete(() =>
                    {
                        switch (enemy.EnemyTypeId)
                        {
                            case EnemyTypeId.SnakeLet or EnemyTypeId.SnakeNaga or EnemyTypeId.Werewolf:
                                _effectOnEnemyActivation.IncreaseOffset(4.5f);
                                break;
                                
                            case EnemyTypeId.Sunflora:
                                _effectOnEnemyActivation.IncreaseOffset(5.5f);
                                break;
                        }

                        _effectOnEnemyActivation.Play(enemy);
                    });
                
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