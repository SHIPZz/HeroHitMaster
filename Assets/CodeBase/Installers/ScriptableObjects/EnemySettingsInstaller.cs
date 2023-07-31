using UnityEngine;
using Zenject;

namespace CodeBase.Installers.ScriptableObjects
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