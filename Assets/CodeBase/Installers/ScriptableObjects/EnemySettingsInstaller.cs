using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "EnemySettingsInstaller", menuName = "Installers/EnemySettingsInstaller")]
public class EnemySettingsInstaller : ScriptableObjectInstaller<EnemySettingsInstaller>
{
    [SerializeField] private EnemySetting _enemySetting;
    
    public override void InstallBindings()
    {
        Container.BindInstance(_enemySetting);
    }
}