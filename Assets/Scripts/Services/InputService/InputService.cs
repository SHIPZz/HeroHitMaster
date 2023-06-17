using Services;
using UnityEngine.InputSystem;

public class InputService : IInputService
{
    private readonly InputActions _inputActions;

    public InputService()
    {
        _inputActions = new InputActions();
    }

    public InputActions.PlayerActions PlayerActions =>
        _inputActions.Player;
    
    public InputAction PlayerMove => 
        PlayerActions.Move;

    public InputAction PlayerJump =>
        PlayerActions.Jump;
}