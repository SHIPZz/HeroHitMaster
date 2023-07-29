using Enums;
using UnityEngine;
using Zenject;
 
namespace Installers.GameObjectInstallers.Weapon
{
    public class WeaponInstaller : MonoInstaller
    {
        [SerializeField, SerializeReference] private Gameplay.Weapons.Weapon _weapon;
        [SerializeField] private WeaponTypeId _weaponTypeId;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<Gameplay.Weapons.Weapon>().FromInstance(_weapon).AsSingle();
        }

    }
}