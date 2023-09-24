using UnityEngine;
using UnityEngine.InputSystem;

namespace CodeBase.Services.Inputs.InputService
{
    public class InputService : IInputService
    {
        private readonly InputActions _inputActions;

        public InputService()
        {
            _inputActions = new InputActions();
            _inputActions.Enable();
        }

        public InputActions.PlayerActions PlayerActions =>
            _inputActions.Player;

        public InputAction PlayerMove =>
            PlayerActions.Move;

        public InputAction PlayerJump =>
            PlayerActions.Jump;

        public InputAction PlayerRun =>
            PlayerActions.Run;

        public InputAction PlayerFire =>
            PlayerActions.Fire;

        public Vector2 MousePosition =>
            PlayerActions.MousePosition.ReadValue<Vector2>();
    }
}