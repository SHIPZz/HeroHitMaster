using UnityEngine;
using Zenject;

public class PlayerCameraFollower : MonoBehaviour
{
    [SerializeField] private float _smoothSpeed;
    [SerializeField] private Vector3 _offset;

    private Player _player;

    [Inject]
    public void Construct(Player player)
    {
        _player = player;
    }
    
    private void LateUpdate()
    {
        Vector3 targetPosition = _player.transform.position + _offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition, _smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;
    }

    public class Factory : PlaceholderFactory<Player, PlayerCameraFollower>
    {
    }
}