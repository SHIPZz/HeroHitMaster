using Gameplay.Character.Player;
using UnityEngine;

[CreateAssetMenu(menuName = "Gameplay/PlayerSettings", fileName = "PlayerSettings")]
public class PlayerSettings : ScriptableObject
{
    [SerializeField] private Player _player;
    [SerializeField] private int _cost;
    [SerializeField] private Animator _animator;
    [SerializeField] private SkinnedMeshRenderer _skinnedMeshRenderer;

    public SkinnedMeshRenderer SkinnedMeshRenderer => _skinnedMeshRenderer;

    public Player Player => _player;

    public int Cost => _cost;

    public Animator Animator => _animator;
}
