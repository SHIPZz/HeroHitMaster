using Services;
using UnityEngine.InputSystem;
using Vector2 = UnityEngine.Vector2;

public class InputService : IInputService
{
    private readonly InputActions _inputActions;

    public InputService()
    {
        _inputActions = new InputActions();
        _inputActions.Player.Enable();
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

    public bool LeftMouseButtonClicked =>
        PlayerActions.Fire.WasPressedThisFrame();

    public Vector2 MousePosition =>
        Mouse.current.position.ReadValue();
}