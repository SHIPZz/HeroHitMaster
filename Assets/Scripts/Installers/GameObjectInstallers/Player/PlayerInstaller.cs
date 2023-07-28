using Gameplay.Character;
using Gameplay.Character.Player;
using Gameplay.Character.Player.Shoot;
using Gameplay.Character.Players;
using Gameplay.Character.Players.Shoot;
using Gameplay.Web;
using UnityEngine;
using Zenject;

namespace Installers.GameObjectInstallers.Player
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField] private Gameplay.Character.Players.Player _player;
        [SerializeField] private Animator _animator;
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private Transform _initialShootPosition;
        [SerializeField] private PlayerHealth _playerHealth;
        [SerializeField] private ShootingOnAnimationEvent _shootingOnAnimationEvent;
        
        public override void InstallBindings()
        {
            BindInstances();
            BindAsSingle();
            BindInterfacesAndSelfTo();
        }

        private void BindInterfacesAndSelfTo()
        {
            Container.BindInterfacesAndSelfTo<PlayerShootPresenter>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerInput>().AsSingle();
            // Container.Bind<WeaponSelector>().AsSingle();
            // Container.BindInterfacesAndSelfTo<WeaponSelectorPresenter>().AsSingle();
        }

        private void BindAsSingle()
        {
            Container.BindInterfacesTo<WebMovement>().AsSingle();
            Container.Bind<PlayerAnimator>().AsSingle();
            Container.Bind<PlayerShoot>().AsSingle();
        }

        private void BindInstances()
        {
            Container.BindInstance(_player);
            Container.BindInstance(_rigidbody);
            Container.BindInstance(_shootingOnAnimationEvent);
            Container.BindInstance(_initialShootPosition);
            Container.BindInstance(_characterController);
            Container.BindInstance(_animator);
            Container.Bind<IHealth>().To<PlayerHealth>().FromInstance(_playerHealth);
        }
    }
}