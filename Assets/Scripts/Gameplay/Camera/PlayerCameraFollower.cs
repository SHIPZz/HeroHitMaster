using Cinemachine;
using Gameplay.Character.Player;
using Services.Providers;
using UnityEngine;
using Zenject;

namespace Gameplay.Camera
{
    public class PlayerCameraFollower : MonoBehaviour
    {
        [SerializeField] private float _smoothSpeed;
        [SerializeField] private Vector3 _offset;

        private PlayerProvider _playerProvider;
        private Player _player;

        private void LateUpdate()
        {
            if(_player is null)
                return;
            
            transform.position = _player.Head.position + new Vector3(0, 0, -0.1f);
            // Vector3 targetPosition = _player.transform.position + _offset;
            // Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition, _smoothSpeed * Time.deltaTime);
            // transform.position = smoothedPosition;
        }

        public void SetPlayer(Player player) =>
            _player = player;
    }
}