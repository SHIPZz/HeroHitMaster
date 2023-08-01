using System.Collections.Generic;
using CodeBase.Services.Storages;
using UnityEngine;
using Zenject;

namespace CodeBase.Installers.ScriptableObjects
{
    [CreateAssetMenu(fileName = "EnemySettingsInstaller", menuName = "Installers/EnemySettingsInstaller")]
    public class EnemySettingsInstaller : ScriptableObjectInstaller<EnemySettingsInstaller>
    {
        [SerializeField] private EnemySetting _enemySetting;
        [SerializeField] private EnemyStaticDataService _enemyStaticDataService;

        public override void InstallBindings()
        {
            Container.BindInstance(_enemySetting);
            Container.BindInstance(_enemyStaticDataService);
        }
    }
}