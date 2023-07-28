using Enums;
using Gameplay;
using Gameplay.Character;
using Gameplay.Character.Enemy;
using Gameplay.EffectPlaying;
using Gameplay.MaterialChanger;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Installers.GameObjectInstallers.Enemy
{
    public class EnemyInstaller : MonoInstaller
    {
        [SerializeField] private SkinnedMeshRenderer _skinnedMeshRenderer;
        [SerializeField] private Animator _animator;
        [SerializeField] private Collider _collider;
        [SerializeField] private EnemyHealth _enemyHealth;
        [SerializeField] private TriggerObserver _triggerObserver;
        [SerializeField] private ParticleSystem _hitEffect;
        [SerializeField] private ParticleSystem _dieEffect;
        [SerializeField] private NavMeshAgent _navMeshAgent;
        [SerializeField] private EnemyFollower _enemyFollower;
        
        public override void InstallBindings()
        {
            BindInstances();

            BindInterfacesAndSelfTo();

            BindEffects();

            Container.Bind<EnemyAnimator>().AsSingle();
            Container.Bind<IMaterialChanger>().To<SkinnedMaterialChanger>()
                .FromComponentOn(gameObject).AsSingle();
        }

        private void BindInstances()
        {
            Container.Bind<IHealth>().To<EnemyHealth>().FromInstance(_enemyHealth).AsSingle();
            Container.BindInstance(_skinnedMeshRenderer);
            Container.BindInstance(_triggerObserver);
            Container.BindInstance(_animator);
            Container.BindInstance(_collider);
            Container.BindInstance(_navMeshAgent);
            Container.BindInstance(_enemyFollower);
            Container.Bind<EnemyAttacker>().AsSingle();
        }

        private void BindInterfacesAndSelfTo()
        {
            Container.BindInterfacesAndSelfTo<EnemyDestroyOnDeath>().AsSingle();
            Container.BindInterfacesAndSelfTo<EffectOnHit>().AsSingle();
            Container.BindInterfacesAndSelfTo<DeathEffectOnHit>().AsSingle();
            Container.BindInterfacesAndSelfTo<NonCollisionOnDeath>().AsSingle();
            Container.BindInterfacesAndSelfTo<SkinnedMeshVisibilityHandler>().AsSingle();
            Container.BindInterfacesAndSelfTo<DestroyEnemyEffectsHandler>().AsSingle();
            Container.BindInterfacesAndSelfTo<AnimOnAgentMoving>().AsSingle();
            Container.BindInterfacesAndSelfTo<AnimOnHit>().AsSingle();
            Container.BindInterfacesAndSelfTo<StopMovementOnHit>().AsSingle();
        }

        private void BindEffects()
        {
            Container.BindInstance(_hitEffect).WithId(ParticleTypeId.HitEffect);
            Container.BindInstance(_dieEffect).WithId(ParticleTypeId.DieEffect);
        }
    }
}