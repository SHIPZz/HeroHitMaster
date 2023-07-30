using Enums;
using UnityEngine;
using Zenject;

namespace CodeBase.Installers.GameObjectInstallers.Weapon
{
    public class WeaponInstaller : MonoInstaller
    {
        [SerializeField, SerializeReference] private CodeBase.Gameplay.Weapons.Weapon _weapon;
        [SerializeField] private WeaponTypeId _weaponTypeId;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<CodeBase.Gameplay.Weapons.Weapon>().FromInstance(_weapon).AsSingle();
        }

    }
}