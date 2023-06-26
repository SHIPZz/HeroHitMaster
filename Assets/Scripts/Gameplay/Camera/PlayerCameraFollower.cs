using Cinemachine;
using Gameplay.Character.Player;
using UnityEngine;
using Zenject;

namespace Gameplay.Camera
{
    public class PlayerCameraFollower : MonoBehaviour
    {
        [SerializeField] private float _smoothSpeed;
        [SerializeField] private Vector3 _offset;

        private Player _player;
        private CinemachineFreeLook _cinemachineFreeLook;

        [Inject]
        public void Construct(Player player)
        {
            _player = player;
            // _cinemachineFreeLook = GetComponent<CinemachineFreeLook>();
            // _cinemachineFreeLook.Follow = _player.transform;
        }

        private void LateUpdate()
        {
            transform.position = _player.Head.position;
            // Vector3 targetPosition = _player.transform.position + _offset;
            // Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition, _smoothSpeed * Time.deltaTime);
            // transform.position = smoothedPosition;
        }

        public class Factory : PlaceholderFactory<Player, PlayerCameraFollower>
        {
        }
    }
}