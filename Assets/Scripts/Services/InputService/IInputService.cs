using UnityEngine.InputSystem;

namespace Services
{
    public interface IInputService
    {
         InputActions.PlayerActions PlayerActions { get; }
         InputAction PlayerMove { get; }
         InputAction PlayerJump { get; }
         InputAction PlayerRun { get; }
         InputAction PlayerFire { get; }
    }
}