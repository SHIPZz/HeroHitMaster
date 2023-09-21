using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace CodeBase.Gameplay.Camera
{
    public class RotateCamera : MonoBehaviour
    {
        private PlayerCameraFollower _cameraFollower;
        private Vector3 _lastCameraRotation;

        private void Awake() =>
            _cameraFollower = GetComponent<PlayerCameraFollower>();

        public async void Do()
        {
            await UniTask.WaitForSeconds(1f);
            
            _lastCameraRotation = new Vector3(transform.rotation.x, transform.rotation.y, transform.rotation.z);
            _cameraFollower.transform
                .DORotate(new Vector3(-100f, transform.rotation.y, transform.rotation.z), 1.5f);
        }

        public void ReturnLastRotation(int obj) =>
            _cameraFollower
                .transform
                .DORotate(_lastCameraRotation, 1.5f);
    }
}