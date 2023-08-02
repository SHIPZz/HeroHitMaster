using CodeBase.ScriptableObjects.Enemy;
using UnityEngine;
using Zenject;

namespace CodeBase.Installers.ScriptableObjects.Enemy
{
    [CreateAssetMenu(fileName = "EnemySettingsInstaller", menuName = "Installers/EnemySettingsInstaller")]
    public class EnemySettingsInstaller : ScriptableObjectInstaller<EnemySettingsInstaller>
    {
        [SerializeField] private EnemySetting _enemySetting;

        public override void InstallBindings()
        {
            Container.BindInstance(_enemySetting);
        }
    }
}