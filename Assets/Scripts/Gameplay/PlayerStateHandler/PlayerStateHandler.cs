using System;
using System.Collections.Generic;
using Gameplay.Character.Player;
using Gameplay.PlayerStateHandler.States;
using Services;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Gameplay.PlayerStateHandler
{
    public class PlayerStateHandler : MonoBehaviour
    {
        private readonly Dictionary<StateId, State> _states = new();
        
        private IInputService _inputService;

        [Inject]
        public void Construct(IInputService inputService)
        {
            _inputService = inputService;
        }

        private void Start()
        {
            AddState(StateId.Idle, gameObject.AddComponent<IdleState>());
            AddState(StateId.Run, gameObject.AddComponent<RunState>());
            AddState(StateId.Walk, gameObject.AddComponent<WalkState>());
        }

        private void OnEnable()
        {
            _inputService.PlayerMove.performed += OnPlayerMovePerformed;
            _inputService.PlayerMove.canceled += OnPlayerMoveCanceled;
            _inputService.PlayerRun.performed += OnPlayerRunPerformed;
            _inputService.PlayerRun.canceled += OnPlayerRunCanceled;
        }

        private void OnDisable()
        {
            _inputService.PlayerMove.performed -= OnPlayerMovePerformed;
            _inputService.PlayerMove.canceled -= OnPlayerMoveCanceled;
            _inputService.PlayerRun.performed -= OnPlayerRunPerformed;
            _inputService.PlayerRun.canceled -= OnPlayerRunCanceled;
        }

        private void OnPlayerRunPerformed(InputAction.CallbackContext obj)
        {
            ActivateState(StateId.Run);
        }

        private void OnPlayerRunCanceled(InputAction.CallbackContext obj)
        {
            ActivateState(StateId.Idle);
        }

        private void OnPlayerMoveCanceled(InputAction.CallbackContext obj)
        {
            ActivateState(StateId.Idle);
        }

        private void OnPlayerMovePerformed(InputAction.CallbackContext obj)
        {
            ActivateState(StateId.Walk);
        }

        private void AddState(StateId stateId, State state)
        {
            _states[stateId] = state;
        }

        private void ActivateState(StateId stateId)
        {
            DisableAllStates();
            
            _states[stateId].Enable();
        }

        private void DisableAllStates()
        {
            foreach (var state in _states)
            {
                state.Value.Exit();
            }
        }
    }
}