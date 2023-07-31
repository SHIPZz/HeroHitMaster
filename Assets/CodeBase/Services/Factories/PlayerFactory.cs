using System.Collections.Generic;
using CodeBase.Constants;
using CodeBase.Enums;
using CodeBase.Gameplay.Character.Players;
using CodeBase.Services.Providers.AssetProviders;
using UnityEngine;
using Zenject;

namespace CodeBase.Services.Factories
{
    public class PlayerFactory
    {
        private readonly AssetProvider _assetProvider;
        private readonly DiContainer _diContainer;
        private readonly Dictionary<PlayerTypeId, string> _characters = new()
        {
            { PlayerTypeId.Spider, AssetPath.Spiderman },
            { PlayerTypeId.Wolverine, AssetPath.Wolverine },
            { PlayerTypeId.Wizard, AssetPath.Wizard },
        };

        public PlayerFactory(AssetProvider assetProvider, DiContainer diContainer)
        {
            _assetProvider = assetProvider;
            _diContainer = diContainer;
        }

        public Player Create(PlayerTypeId playerTypeId, Vector3 at)
        {
            if (!_characters.TryGetValue(playerTypeId, out var path))
            {
                Debug.Log("ERROR");
                return null;
            }

            return Create(path, at);
        }

        private Player Create(string path, Vector3 at)
        {
            GameObject player = _assetProvider.GetAsset(path);
            return _diContainer.InstantiatePrefabForComponent<Player>(player, at, Quaternion.identity,null);
        }
    }
}