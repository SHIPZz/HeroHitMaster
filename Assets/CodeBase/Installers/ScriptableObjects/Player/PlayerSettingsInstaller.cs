using System.Collections.Generic;
using CodeBase.Installers.GameObjectInstallers.Player;
using Enums;
using ScriptableObjects.PlayerSettings;
using UnityEngine;
using Zenject;

namespace CodeBase.Installers.ScriptableObjects.Player
{
    [CreateAssetMenu(fileName = "PlayerSettingsInstaller", menuName = "Installers/PlayerSettingsInstaller")]
    public class PlayerSettingsInstaller : ScriptableObjectInstaller<PlayerSettingsInstaller>
    {
        [SerializeField] private PlayerSettings _playerSettings;
        
        public override void InstallBindings()
        {
            Container
                .BindInstance(_playerSettings)
                .AsSingle();
        }
    }
}