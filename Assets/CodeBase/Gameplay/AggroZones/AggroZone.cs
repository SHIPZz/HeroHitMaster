using System;
using CodeBase.Gameplay.Character.Players;
using CodeBase.Gameplay.Collision;
using UnityEngine;

public class AggroZone : MonoBehaviour
{
    [SerializeField] private TriggerObserver _triggerObserver;

    public event Action<Player> PlayerEntered;
    
    private void OnEnable() => 
        _triggerObserver.Entered += OnPlayerEntered;

    private void OnDisable() => 
        _triggerObserver.Entered -= OnPlayerEntered;

    private void OnPlayerEntered(Collider player) => 
        PlayerEntered?.Invoke(player.GetComponent<Player>());
}
