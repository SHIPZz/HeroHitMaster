using System;
using CodeBase.Gameplay.Character.Players;
using UnityEngine;

namespace CodeBase.Gameplay
{
    [RequireComponent(typeof(AggroZone))]
    public class EnableUIOnPlayerAggroZoneEnter : MonoBehaviour
    {
        [SerializeField] private Canvas _mainCanvas;

        private AggroZone _aggroZone;

        private void Awake()
        {
            _mainCanvas.enabled = false;
            _aggroZone = GetComponent<AggroZone>();
        }

        private void OnEnable() => 
            _aggroZone.PlayerEntered += Turn;

        private void OnDisable() => 
            _aggroZone.PlayerEntered -= Turn;

        private void Turn(Player obj) => 
            _mainCanvas.enabled = true;
    }
}