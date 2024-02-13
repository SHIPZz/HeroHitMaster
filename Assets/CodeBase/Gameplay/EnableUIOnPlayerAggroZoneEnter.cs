using System;
using CodeBase.Gameplay.Character.Players;
using CodeBase.Services.Pause;
using CodeBase.Services.UI;
using UnityEngine;
using Zenject;

namespace CodeBase.Gameplay
{
    [RequireComponent(typeof(AggroZone))]
    public class EnableUIOnPlayerAggroZoneEnter : MonoBehaviour
    {
        private AggroZone _aggroZone;
        private UIService _uiService;
        private IPauseService _pauseService;

        [Inject]
        private void Construct(UIService uiService, IPauseService pauseService)
        {
            _pauseService = pauseService;
            _uiService = uiService;
        }
        
        private void Awake() => 
            _aggroZone = GetComponent<AggroZone>();

        private void OnEnable()
        {
            _aggroZone.PlayerEntered += Turn;
            _pauseService.UnPause();
        }

        private void Start() => 
            _uiService.BlockEnablingMainUI();

        private void OnDisable() => 
            _aggroZone.PlayerEntered -= Turn;

        private void Turn(Player obj)
        {
            _uiService.UnBlock();
        }
    }
}