using Gameplay.Character.Enemy;
using Gameplay.Character.Player;
using Gameplay.Character.Player.Shoot;
using Gameplay.PlayerSelection;
using Gameplay.Web;
using UI;
using UnityEngine;
using Zenject;

namespace Installers.GameObjectInstallers.Player
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField] private Gameplay.Character.Player.Player _player;
        [SerializeField] private Animator _animator;
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private Transform _initialShootPosition;
        [SerializeField] private CharacterHealth _characterHealth;
        
        public override void InstallBindings()
        {
            BindInstances();
            BindAsSingle();
            BindInterfacesAndSelfTo();
        }

        private void BindInterfacesAndSelfTo()
        {
            Container.BindInterfacesAndSelfTo<PlayerShootMediator>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerInput>().AsSingle();
            Container.BindInterfacesTo<PlayerMediator>().AsSingle();
            // Container.Bind<WeaponSelector>().AsSingle();
            // Container.BindInterfacesAndSelfTo<WeaponSelectorPresenter>().AsSingle();
        }

        private void BindAsSingle()
        {
            Container.BindInterfacesTo<WebMovement>().AsSingle();
            Container.Bind<PlayerMovement>().AsSingle();
            Container.Bind<PlayerAnimator>().AsSingle();
            Container.Bind<PlayerShoot>().AsSingle();
        }

        private void BindInstances()
        {
            Container.BindInstance(_player);
            Container.BindInstance(_rigidbody);
            Container.BindInstance(_initialShootPosition);
            Container.BindInstance(_characterController);
            Container.BindInstance(_animator);
            Container.BindInstance(_characterHealth);
        }
    }
}