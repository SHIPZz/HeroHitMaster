using CodeBase.Gameplay.Camera;
using CodeBase.Gameplay.Collision;
using CodeBase.Services.Providers;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace CodeBase.Gameplay.Character.Players
{
    public class RotatePlayerOnTriggerEnter : MonoBehaviour
    {
        [SerializeField] private TriggerObserver _triggerObserver;
        [SerializeField] private Transform _targetAngleTransform;

        private UnityEngine.Camera _camera;
        private IProvider<CameraData> _cameraProvider;

        [Inject]
        private void Construct(IProvider<CameraData> cameraProvider)
        {
            _cameraProvider = cameraProvider;
        }

        private void OnEnable() =>
            _triggerObserver.Entered += Rotate;

        private void OnDisable() =>
            _triggerObserver.Entered -= Rotate;

        private async void Rotate(Collider player)
        {
            Vector3 directionToTarget = _targetAngleTransform.position - player.transform.position;

            float angle = Mathf.Atan2(directionToTarget.x, directionToTarget.z) * Mathf.Rad2Deg;
            await UniTask.WaitForSeconds(0.1f);
            player.transform.DORotate(new Vector3(0, angle, 0), 0.3f);

            _cameraProvider.Get().Rotator.transform.DOLocalRotate(new Vector3(0, angle, 0), 0.3f);
        }
    }
}