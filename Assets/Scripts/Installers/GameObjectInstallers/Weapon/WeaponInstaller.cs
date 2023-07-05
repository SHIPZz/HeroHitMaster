using Gameplay.Bullet;
using Sirenix.Serialization;
using UnityEngine;
using Zenject;

namespace Installers.GameObjectInstallers.Weapon
{
    public class WeaponInstaller : MonoInstaller
    {
        [SerializeField, SerializeReference] private Gameplay.Weapon.Weapon _weapon;
        
        public override void InstallBindings()
        {
            Container
                .BindInterfacesAndSelfTo<BulletSticking>()
                .AsSingle();

            Container
                .BindInterfacesAndSelfTo<Gameplay.Weapon.Weapon>().FromInstance(_weapon)
                .AsSingle();
        }
    }
}