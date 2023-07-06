using Gameplay.Character.Enemy;
using Gameplay.MaterialChanger;
using UnityEngine;
using Zenject;

namespace Installers.GameObjectInstallers.Enemy
{
    public class EnemyInstaller : MonoInstaller
    {
        [SerializeField] private SkinnedMeshRenderer _skinnedMeshRenderer;
        [SerializeField] private Animator _animator;
        [SerializeField] private Collider _collider;
        [SerializeField] private EnemyHealth _enemyHealth;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<EnemyDestroyOnDeath>().AsSingle();
            Container.BindInterfacesAndSelfTo<EnemyHealth>().FromInstance(_enemyHealth).AsSingle();
            Container.BindInstance(_skinnedMeshRenderer);
            Container.Bind<IMaterialChanger>().To<SkinnedMaterialChanger>()
                .FromComponentOn(gameObject).AsSingle();

            Container.BindInstance(_animator);
            Container.BindInstance(_collider);
        }
    }
}