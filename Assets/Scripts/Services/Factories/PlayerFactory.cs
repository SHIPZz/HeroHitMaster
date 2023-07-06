using System.Collections.Generic;
using Constants;
using Enums;
using Gameplay.Character.Player;
using Services.Providers.AssetProviders;
using UnityEngine;
using Zenject;

namespace Services.Factories
{
    public class PlayerFactory
    {
        private readonly AssetProvider _assetProvider;
        private readonly DiContainer _diContainer;
        private Dictionary<PlayerTypeId, string> _characters;

        public PlayerFactory(AssetProvider assetProvider, DiContainer diContainer)
        {
            _assetProvider = assetProvider;
            _diContainer = diContainer;

            _characters = new Dictionary<PlayerTypeId, string>
            {
                { PlayerTypeId.Spider, AssetPath.Spiderman },
                { PlayerTypeId.Wolverine, AssetPath.Wolverine },
            };
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
            var player = _assetProvider.GetAsset(path);
            return _diContainer.InstantiatePrefabForComponent<Player>(player, at, Quaternion.identity,null);
        }
    }
}