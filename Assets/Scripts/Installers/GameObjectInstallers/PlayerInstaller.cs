using Enums;
using Gameplay.Character.Player;
using Gameplay.Character.Player.Shoot;
using Gameplay.Web;
using ScriptableObjects.WebSettings;
using Services;
using Services.Inputs.Weapon;
using UnityEngine;
using Zenject;

public class PlayerInstaller : MonoInstaller
{
    [SerializeField] private Player _player;
    [SerializeField] private Animator _animator;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private Transform _rightHand;

    private Vector3 _at;
    private WebSettings _webSettings;

    [Inject]
    public void Construct(Vector3 at)
    {
        _at = at;
        // _webSettings = webSettings;
    }

    public override void InstallBindings()
    {
        Container.BindInstance(_player);
        Container.BindInstance(_at).WhenInjectedInto<Player>();
        Container.BindInstance(_webSettings);
        Container.BindInstance(_rigidbody);
        Container.BindInstance(_rightHand);
        Container.BindInstance(_characterController);
        Container.BindInstance(_animator);
        Container.Bind<WebMovement>().AsSingle();
        Container.Bind<WeaponSelectorHandler>().AsSingle();
        Container.Bind<PlayerMovement>().AsSingle();
        Container.Bind<PlayerAnimation>().AsSingle();
        Container.BindInterfacesAndSelfTo<ShootMediator>().AsSingle();
        Container.BindInterfacesAndSelfTo<ShootHand>().AsSingle();
        Container.BindInterfacesTo<PlayerMediator>().AsSingle();
    }
}