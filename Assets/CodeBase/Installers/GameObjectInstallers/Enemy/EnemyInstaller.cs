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
        [SerializeField] private EnemyHealth _enemyHealth;
        [SerializeField] private ParticleSystem _hitEffect;
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
                .BindInterfacesAndSelfTo<EnemyMovementPresenter>()
                .AsSingle();

            Container
                .BindInterfacesAndSelfTo<EnemyDeathEffectOnDestruction>()
                .AsSingle();

            Container
                .BindInterfacesAndSelfTo<DeathAnimOnMaterialChange>()
                .AsSingle();
        }

        private void BindInstances()
        {
            Container.Bind<IHealth>().To<EnemyHealth>().FromInstance(_enemyHealth).AsSingle();
            Container.BindInstance(GetComponent<TriggerObserver>());
            Container.BindInstance(_enemy);
            Container.BindInstance(_enemy.EnemyTypeId);
            Container.BindInstance(GetComponent<Animator>());
            Container.BindInstance(GetComponent<Collider>());
            Container.BindInstance(GetComponent<NavMeshAgent>());
            Container.BindInstance(_enemyFollower);
            Container.BindInstance(GetComponent<DieOnAnimationEvent>());
            Container.Bind<EnemyAttacker>().AsSingle();
        }

        private void BindInterfacesAndSelfTo()
        {
            Container.BindInterfacesAndSelfTo<EffectOnHit>().AsSingle();
            Container.BindInterfacesAndSelfTo<DeathSoundOnHit>().AsSingle();
            Container.BindInterfacesAndSelfTo<NonCollisionOnDeath>().AsSingle();
            Container.BindInterfacesAndSelfTo<DestroyEnemyEffectsHandler>().AsSingle();
            Container.BindInterfacesAndSelfTo<AnimOnAgentMoving>().AsSingle();
            Container.BindInterfacesAndSelfTo<AnimOnHit>().AsSingle();
            Container.BindInterfacesAndSelfTo<WinAnimOnPlayerDeath>().AsSingle();
            Container.BindInterfacesAndSelfTo<StopMovementOnHit>().AsSingle();
            Container.BindInterfacesAndSelfTo<StopMovementOnMaterialChange>().AsSingle();
            Container.BindInterfacesAndSelfTo<EnemyBodyPartPositionSetter>().AsSingle();
            Container.BindInterfacesAndSelfTo<EnemyBodyPartMediator>().AsSingle();
        }

        private void BindEffects()
        {
            Container.BindInstance(_hitEffect).WithId(ParticleTypeId.EnemyHitEffect);
        }
    }
}