using UnityEngine.InputSystem;
using Vector2 = UnityEngine.Vector2;

namespace Services.Inputs
{
    public interface IInputService
    {
        InputActions.PlayerActions PlayerActions { get; }
        InputAction PlayerMove { get; }
        InputAction PlayerJump { get; }
        InputAction PlayerRun { get; }
        InputAction PlayerFire { get; }
        Vector2 MousePosition { get; }
    }
}