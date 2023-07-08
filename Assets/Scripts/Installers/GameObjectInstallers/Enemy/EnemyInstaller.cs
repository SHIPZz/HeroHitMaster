using Enums;
using Gameplay;
using Gameplay.Character;
using Gameplay.Character.Enemy;
using Gameplay.EffectPlaying;
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
        [SerializeField] private CharacterHealth characterHealth;
        [SerializeField] private TriggerObserver _triggerObserver;
        [SerializeField] private ParticleSystem _hitEffect;
        [SerializeField] private ParticleSystem _dieEffect;

        public override void InstallBindings()
        {
            BindInstances();
            
            BindInterfacesAndSelfTo();

            BindEffects();

            Container.Bind<IMaterialChanger>().To<SkinnedMaterialChanger>()
                .FromComponentOn(gameObject).AsSingle();
            
            Container.Bind<IHealth>().FromMethod(context => context.Container.Resolve<CharacterHealth>().Health);
        }

        private void BindInstances()
        {
            Container.BindInstance(_skinnedMeshRenderer);
            Container.BindInstance(_triggerObserver);
            Container.BindInstance(_animator);
            Container.BindInstance(_collider);
        }

        private void BindInterfacesAndSelfTo()
        {
            Container.BindInterfacesAndSelfTo<CharacterHealth>().FromInstance(characterHealth).AsSingle();
            Container.BindInterfacesAndSelfTo<EnemyDestroyOnDeath>().AsSingle();
            Container.BindInterfacesAndSelfTo<EffectOnHit>().AsSingle();
            Container.BindInterfacesAndSelfTo<DeathEffectOnHit>().AsSingle();
            Container.BindInterfacesAndSelfTo<NonCollisionOnDeath>().AsSingle();
            Container.BindInterfacesAndSelfTo<SkinnedMeshVisibilityHandler>().AsSingle();
            Container.BindInterfacesAndSelfTo<DestroyEnemyEffectsHandler>().AsSingle();
        }

        private void BindEffects()
        {
            Container.BindInstance(_hitEffect).WithId(ParticleTypeId.HitEffect);
            Container.BindInstance(_dieEffect).WithId(ParticleTypeId.DieEffect);
        }
    }
}