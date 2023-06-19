using ScriptableObjects;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "SettingsScriptableObjectInstaller", menuName = "Installers/SettingsScriptableObjectInstaller")]
public class SettingsScriptableObjectInstaller : ScriptableObjectInstaller<SettingsScriptableObjectInstaller>
{
    [SerializeField] private PlayerMovementSettings _playerMovementSettings;
    
    public override void InstallBindings()
    {
        Container.BindInstance(_playerMovementSettings);
    }
}