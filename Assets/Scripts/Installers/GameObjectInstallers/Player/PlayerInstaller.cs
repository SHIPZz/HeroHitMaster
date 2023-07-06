using Gameplay.Character.Player;
using Gameplay.Character.Player.Shoot;
using Gameplay.Web;
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
    

        public override void InstallBindings()
        {
            Container.BindInstance(_player);
            Container.BindInstance(_rigidbody);
            Container.BindInstance(_initialShootPosition);
            Container.BindInstance(_characterController);
            Container.BindInstance(_animator);
            Container.Bind<WebMovement>().AsSingle();
            Container.Bind<PlayerMovement>().AsSingle();
            Container.Bind<PlayerAnimator>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerShootMediator>().AsSingle();
            Container.Bind<PlayerShoot>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerInput>().AsSingle();
            Container.BindInterfacesTo<PlayerMediator>().AsSingle();
        }
    }
}