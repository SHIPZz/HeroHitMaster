using System.Collections.Generic;
using System.Linq;
using CodeBase.Enums;
using CodeBase.Gameplay.Character.Players;
using UnityEngine;
using Zenject;

namespace CodeBase.Services.Factories
{
    public class PlayerFactory
    {
        private readonly DiContainer _diContainer;
        private readonly Dictionary<PlayerTypeId, Player> _characters;

        public PlayerFactory( DiContainer diContainer)
        {
            _diContainer = diContainer;
            _characters = Resources.LoadAll<Player>("Prefabs/Player")
                .ToDictionary(x => x.PlayerTypeId, x => x);
        }

        public Player Create(PlayerTypeId playerTypeId, Vector3 at)
        {
            Player player = _characters[playerTypeId];
            return _diContainer.InstantiatePrefabForComponent<Player>(player, at, Quaternion.identity,null);
        }
    }
}