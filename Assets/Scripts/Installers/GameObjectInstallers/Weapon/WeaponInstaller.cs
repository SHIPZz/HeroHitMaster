using Gameplay.Bullet;
using Gameplay.Character.Player.Shoot;
using UnityEngine;
using Zenject;

namespace Installers.GameObjectInstallers.Weapon
{
    public class WeaponInstaller : MonoInstaller
    {
        [SerializeField, SerializeReference] private Gameplay.Weapon.Weapon _weapon;
        [SerializeField] private AudioSource _audioSource;
        
        public override void InstallBindings()
        {
            // Container.BindInterfacesAndSelfTo<BulletSticking>().AsSingle();
            Container.BindInstance(_audioSource);
            Container.BindInterfacesAndSelfTo<Gameplay.Weapon.Weapon>().FromInstance(_weapon).AsSingle();

            Container.Bind<EffectOnShoot>().AsSingle();
        }
    }
}