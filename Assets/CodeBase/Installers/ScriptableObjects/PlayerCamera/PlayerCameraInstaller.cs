using CodeBase.Installers.ScriptableObjects.PlayerCamera;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "PlayerCameraInstaller", menuName = "Installers/PlayerCameraInstaller")]
public class PlayerCameraInstaller : ScriptableObjectInstaller<PlayerCameraInstaller>
{
    [SerializeField] private PlayerCameraSettings _playerCameraSettings;
    
    public override void InstallBindings()
    {
        Container.BindInstance(_playerCameraSettings);
    }
}