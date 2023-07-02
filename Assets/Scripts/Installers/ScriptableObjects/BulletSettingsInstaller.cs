using System.Collections.Generic;
using ScriptableObjects.WebSettings;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "BulletSettingsInstaller", menuName = "Installers/BulletSettingsInstaller")]
public class BulletSettingsInstaller : ScriptableObjectInstaller<BulletSettingsInstaller>
{
    [SerializeField] private List<BulletSettings> _bulletSettings;

    public override void InstallBindings()
    {
        Container
            .BindInstance(_bulletSettings)
            .AsSingle();
    }
}