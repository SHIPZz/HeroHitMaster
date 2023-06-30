using System.Collections.Generic;
using ScriptableObjects.PlayerSettings;
using UnityEngine;
using Zenject;

namespace Installers.ScriptableObjects.Player
{
    [CreateAssetMenu(fileName = "PlayerSettingsInstaller", menuName = "Installers/PlayerSettingsInstaller")]
    public class PlayerSettingsInstaller : ScriptableObjectInstaller<PlayerSettingsInstaller>
    {
        [SerializeField] private List<PlayerSettings> _playerSettings;
        
        public override void InstallBindings()
        {
            Container
                .BindInstance(_playerSettings)
                .AsSingle();
        }
    }
}