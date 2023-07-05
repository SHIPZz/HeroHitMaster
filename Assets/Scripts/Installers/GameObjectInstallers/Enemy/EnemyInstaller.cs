using Gameplay.Character.Enemy;
using UnityEngine;
using Zenject;

namespace Installers.GameObjectInstallers.Enemy
{
    public class EnemyInstaller : MonoInstaller
    {
        [SerializeField] private SkinnedMeshRenderer _skinnedMeshRenderer;
        [SerializeField] private Animator _animator;
        [SerializeField] private Collider _collider;
        [SerializeField] private EnemyView _enemyView;
        [SerializeField] private Gameplay.Character.Enemy.Enemy _enemy;
        [SerializeField] private Material _material;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<EnemyView>().FromInstance(_enemyView).AsSingle();
            Container.BindInterfacesAndSelfTo<Gameplay.Character.Enemy.Enemy>().FromInstance(_enemy).AsSingle();
            Container.BindInstance(_skinnedMeshRenderer);
            Container.BindInstance(_animator);
            Container.BindInstance(_collider);
            Container.BindInstance(_material);

            Container.BindInterfacesAndSelfTo<EnemyPresenter>().AsSingle();
            Container.BindInterfacesAndSelfTo<EnemyDeath>().AsSingle();
        }
    }
}