using System.Collections.Generic;
using Enums;
using UnityEngine;
using Zenject;

namespace Installers.ScriptableObjects.Player
{
    [CreateAssetMenu(fileName = "PlayerSettingsInstaller", menuName = "Installers/PlayerSettingsInstaller")]
    public class PlayerSettingsInstaller : ScriptableObjectInstaller<PlayerSettingsInstaller>
    {
        [SerializeField] private List<PlayerTypeId> _playerTypeIds;
        
        public override void InstallBindings()
        {
            Container
                .BindInstance(_playerTypeIds)
                .AsSingle();
        }
    }
}