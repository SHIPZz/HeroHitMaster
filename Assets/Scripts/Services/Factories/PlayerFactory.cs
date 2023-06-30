using System.Collections.Generic;
using Constants;
using Enums;
using Gameplay.Character.Player;
using Services.Providers.AssetProviders;
using UnityEngine;

namespace Services.Factories
{
    public class PlayerFactory
    {
        private readonly AssetProvider _assetProvider;
        private Dictionary<PlayerTypeId, string> _characters;

        public PlayerFactory(AssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
            
            _characters = new()
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
            var player = _assetProvider.GetAsset<Player>(path);
            return Object.Instantiate(player, at, Quaternion.identity);
        }
    }
}