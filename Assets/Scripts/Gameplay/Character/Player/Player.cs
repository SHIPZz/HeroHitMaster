using System;
using Services;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class Player : MonoBehaviour
{
    private IInputService _inputService;

    private InputAction _playerMove;
    private Vector3 _at;

    [Inject]
    public void Construct(IInputService inputService,Vector3 at)
    {
        _at = at;
        _inputService = inputService;
        _playerMove = _inputService.PlayerMove;
        
        _inputService.PlayerActions.Enable();
    }

    private void Awake()
    {
        transform.position = _at;
    }

    private void Update()
    {
        Move();
        Jump();
    }

    private void Jump()
    {
        if (_inputService.PlayerJump.WasPressedThisFrame())
        {
            transform.Translate(0, 1,0);
        }
    }

    private void Move()
    {
        var direction = _playerMove.ReadValue<Vector2>();
        var direction3d = new Vector3(direction.x, 0, direction.y);
        transform.Translate(direction3d * 30 * Time.deltaTime);
    }

    public class Factory : PlaceholderFactory<Vector3, Player> { }
}