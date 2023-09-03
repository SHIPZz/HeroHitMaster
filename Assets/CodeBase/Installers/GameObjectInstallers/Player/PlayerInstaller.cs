using CodeBase.Enums;
using CodeBase.Gameplay.Bullet.Web;
using CodeBase.Gameplay.Character;
using CodeBase.Gameplay.Character.Enemy;
using CodeBase.Gameplay.Character.Players;
using CodeBase.Gameplay.Character.Players.Shoot;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace CodeBase.Installers.GameObjectInstallers.Player
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField] private CodeBase.Gameplay.Character.Players.Player _player;
        [SerializeField] private Animator _animator;
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private Transform _initialShootPosition;
        [SerializeField] private PlayerHealth _playerHealth;
        [SerializeField] private ShootingOnAnimationEvent _shootingOnAnimationEvent;
        [SerializeField] private PlayerTypeId _playerTypeId;
        [SerializeField] private NavMeshAgent _navMeshAgent;

        public override void InstallBindings()
        {
            BindInstances();
            BindAsSingle();
            BindInterfacesAndSelfTo();
        }

        private void BindInterfacesAndSelfTo()
        {
            Container.BindInterfacesAndSelfTo<PlayerShootPresenter>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerShootInput>().AsSingle();
            Container.BindInterfacesAndSelfTo<EffectOnShoot>().AsSingle();
            Container.BindInterfacesAndSelfTo<DisablePlayerOnLevelFinish>().AsSingle();
        }

        private void BindAsSingle()
        {
            Container.Bind<PlayerAnimator>().AsSingle();
            Container.Bind<PlayerShoot>().AsSingle();
            Container.Bind<PlayerMovement>().FromInstance(GetComponent<PlayerMovement>());
        }

        private void BindInstances()
        {
            Container.BindInstance(_player);
            Container.BindInstance(_rigidbody);
            Container.BindInstance(_shootingOnAnimationEvent);
            Container.BindInstance(_initialShootPosition);
            Container.BindInstance(_animator);
            Container.BindInstance(_playerTypeId);
            Container.BindInstance(_navMeshAgent);
            Container.Bind<IHealth>().To<PlayerHealth>().FromInstance(_playerHealth);
        }
    }
}