using System.Collections.Generic;
using Enums;
using Gameplay.Character.Player;
using Gameplay.Character.Players;
using UnityEngine;

namespace ScriptableObjects.PlayerSettings
{
    [CreateAssetMenu(menuName = "Gameplay/PlayerSettings", fileName = "PlayerSettings")]
    public class PlayerSettings : ScriptableObject
    {
        [SerializeField] private List<PlayerTypeId> _playerTypeIds;
        [SerializeField] private Player _playerPrefab;
        [SerializeField] private PlayerTypeId _playerTypeId;

        public Player PlayerPrefab => _playerPrefab;
        public PlayerTypeId PlayerTypeId => _playerTypeId;

        public IReadOnlyList<PlayerTypeId> PlayerTypeIds => _playerTypeIds;
    }
}