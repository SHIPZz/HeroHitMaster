using System.Collections.Generic;
using CodeBase.ScriptableObjects;
using UnityEngine;
using Zenject;

namespace CodeBase.Installers.ScriptableObjects.Gun
{
    [CreateAssetMenu(fileName = "WeaponSettingsInstaller", menuName = "Installers/WeaponSettingsInstaller")]
    public class WeaponSettingsInstaller : ScriptableObjectInstaller<WeaponSettingsInstaller>
    {
        [SerializeField] private WeaponSettings _weaponSettings;

        public override void InstallBindings()
        {
            Container.BindInstance(_weaponSettings).AsSingle();
        }
    }
}