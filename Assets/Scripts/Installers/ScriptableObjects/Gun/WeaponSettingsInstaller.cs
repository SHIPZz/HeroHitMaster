using System.Collections.Generic;
using ScriptableObjects;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "WeaponSettingsInstaller", menuName = "Installers/WeaponSettingsInstaller")]
public class WeaponSettingsInstaller : ScriptableObjectInstaller<WeaponSettingsInstaller>
{
    [SerializeField] private List<WeaponSettings> _weaponSettings;

    public override void InstallBindings()
    {
        Container.BindInstance(_weaponSettings).AsSingle();
    }
}