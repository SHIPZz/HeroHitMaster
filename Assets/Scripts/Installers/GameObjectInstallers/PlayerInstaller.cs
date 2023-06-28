using Gameplay.Character.Player;
using Gameplay.Character.Player.Shoot;
using Gameplay.Web;
using ScriptableObjects.WebSettings;
using Services.WeaponSelection;
using UnityEngine;
using Zenject;

public class PlayerInstaller : MonoInstaller
{
    [SerializeField] private Player _player;
    [SerializeField] private Animator _animator;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private Transform _initialShootPosition;
    // [SerializeField] private ShootHand _shootHand;

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
        Container.BindInstance(_initialShootPosition);
        // Container.BindInstance(_shootHand);
        Container.BindInstance(_characterController);
        Container.BindInstance(_animator);
        Container.Bind<WebMovement>().AsSingle();
        Container.Bind<WeaponSelector>().AsSingle();
        Container.Bind<PlayerMovement>().AsSingle();
        Container.Bind<PlayerAnimation>().AsSingle();
        Container.BindInterfacesAndSelfTo<ShootInputMediator>().AsSingle();
        Container.BindInterfacesAndSelfTo<WeaponSelectorPresenter>().AsSingle();
        Container.BindInterfacesTo<PlayerMediator>().AsSingle();
    }
}