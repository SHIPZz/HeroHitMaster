using CodeBase.Enums;
using CodeBase.Gameplay.Character;
using CodeBase.Gameplay.Character.Enemy;
using CodeBase.Gameplay.Collision;
using CodeBase.Gameplay.EnemyBodyParts;
using CodeBase.Gameplay.MaterialChanger;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace CodeBase.Installers.GameObjectInstallers.Enemy
{
    public class EnemyInstaller : MonoInstaller
    {
        [SerializeField] private SkinnedMeshRenderer _skinnedMeshRenderer;
        [SerializeField] private Animator _animator;
        [SerializeField] private Collider _collider;
        [SerializeField] private EnemyHealth _enemyHealth;
        [SerializeField] private TriggerObserver _triggerObserver;
        [SerializeField] private ParticleSystem _hitEffect;
        [SerializeField] private NavMeshAgent _navMeshAgent;
        [SerializeField] private EnemyFollower _enemyFollower;
        [SerializeField] private CodeBase.Gameplay.Character.Enemy.Enemy _enemy;

        public override void InstallBindings()
        {
            BindInstances();

            BindInterfacesAndSelfTo();

            BindEffects();

            Container.Bind<EnemyAnimator>().AsSingle();
            Container.Bind<IMaterialChanger>().To<SkinnedMaterialChanger>()
                .FromComponentOn(gameObject).AsSingle();
            Container
                .BindInterfacesAndSelfTo<ActivateEnemyMovementOnFire>()
                .AsSingle();

            Container
                .BindInterfacesAndSelfTo<EnemyBodyPartMediator>()
                .AsSingle();
                      
            Container
                .BindInterfacesAndSelfTo<EnemyDeathEffectOnDestruction>()
                .AsSingle();

            Container.Bind<EnemyBodyPartPositionSetter>().AsSingle();
            Container.Bind<EnemyBodyPartActivator>().AsSingle();
        }

        private void BindInstances()
        {
            Container.Bind<IHealth>().To<EnemyHealth>().FromInstance(_enemyHealth).AsSingle();
            Container.BindInstance(_skinnedMeshRenderer);
            Container.BindInstance(_triggerObserver);
            Container.BindInstance(_enemy);
            Container.BindInstance(_enemy.EnemyTypeId);
            Container.BindInstance(_animator);
            Container.BindInstance(_collider);
            Container.BindInstance(_navMeshAgent);
            Container.BindInstance(_enemyFollower);
            Container.BindInstance(GetComponent<DieOnAnimationEvent>());
            Container.Bind<EnemyAttacker>().AsSingle();
        }

        private void BindInterfacesAndSelfTo()
        {
            Container.BindInterfacesAndSelfTo<EffectOnHit>().AsSingle();
            Container.BindInterfacesAndSelfTo<DeathSoundOnHit>().AsSingle();
            Container.BindInterfacesAndSelfTo<NonCollisionOnDeath>().AsSingle();
            Container.BindInterfacesAndSelfTo<SkinnedMeshVisibilityHandler>().AsSingle();
            Container.BindInterfacesAndSelfTo<DestroyEnemyEffectsHandler>().AsSingle();
            Container.BindInterfacesAndSelfTo<AnimOnAgentMoving>().AsSingle();
            Container.BindInterfacesAndSelfTo<AnimOnHit>().AsSingle();
            Container.BindInterfacesAndSelfTo<WinAnimOnPlayerDeath>().AsSingle();
            Container.BindInterfacesAndSelfTo<StopMovementOnHit>().AsSingle();
            Container.BindInterfacesAndSelfTo<EnemyBodyActivatorDisabler>().AsSingle();
        }

        private void BindEffects()
        {
            Container.BindInstance(_hitEffect).WithId(ParticleTypeId.EnemyHitEffect);
        }
    }
}