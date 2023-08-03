using UnityEngine;
using Zenject;

namespace CodeBase.Installers.ScriptableObjects.PlayerCamera
{
    [CreateAssetMenu(fileName = "PlayerCameraInstaller", menuName = "Installers/PlayerCameraInstaller")]
    public class PlayerCameraInstaller : ScriptableObjectInstaller<PlayerCameraInstaller>
    {
        [SerializeField] private PlayerCameraSettings _playerCameraSettings;
    
        public override void InstallBindings()
        {
            Container.BindInstance(_playerCameraSettings);
        }
    }
}