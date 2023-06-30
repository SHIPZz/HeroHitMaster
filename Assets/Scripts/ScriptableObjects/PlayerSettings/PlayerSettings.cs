using Enums;
using Gameplay.Character.Player;
using UnityEngine;

namespace ScriptableObjects.PlayerSettings
{
    [CreateAssetMenu(menuName = "Gameplay/PlayerSettings", fileName = "PlayerSettings")]
    public class PlayerSettings : ScriptableObject
    {
        [SerializeField] private Player _playerPrefab;
        [SerializeField] private Player _playerViewPrefab;
        [SerializeField] private int _cost;
        [SerializeField] private Animator _animator;
        [SerializeField] private SkinnedMeshRenderer _skinnedMeshRenderer;
        [SerializeField] private PlayerTypeId playerTypeId;

        public SkinnedMeshRenderer SkinnedMeshRenderer => _skinnedMeshRenderer;
        public Player PlayerPrefab => _playerPrefab;
        public int Cost => _cost;
        public Player PlayerViewPrefab => _playerViewPrefab;
        public PlayerTypeId PlayerTypeId => playerTypeId;
        public Animator Animator => _animator;
    }
}