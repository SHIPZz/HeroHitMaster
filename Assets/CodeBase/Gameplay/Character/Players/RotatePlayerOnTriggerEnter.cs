using CodeBase.Gameplay.Camera;
using CodeBase.Gameplay.Collision;
using CodeBase.Services.Pause;
using CodeBase.Services.Providers;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace CodeBase.Gameplay.Character.Players
{
    public class RotatePlayerOnTriggerEnter : MonoBehaviour
    {
        [SerializeField] private Transform _targetAngleTransform;
        [SerializeField] private float _rotateTime = 0.3f;

        private UnityEngine.Camera _camera;
        private TriggerObserver _triggerObserver;
        private IProvider<CameraData> _cameraProvider;
        private bool _isBlocked;
        private RotateCameraOnLastEnemyKilled _rotateCameraOnLastEnemyKilled;
        private IPauseService _pauseService;

        [Inject]
        private void Construct(IProvider<CameraData> cameraProvider,
            RotateCameraOnLastEnemyKilled rotateCameraOnLastEnemyKilled, IPauseService pauseService)
        {
            _pauseService = pauseService;
            _rotateCameraOnLastEnemyKilled = rotateCameraOnLastEnemyKilled;
            _cameraProvider = cameraProvider;
        }

        private void Awake() =>
            _triggerObserver = GetComponent<TriggerObserver>();

        private void OnEnable()
        {
            _pauseService.UnPause();
            _triggerObserver.Entered += Rotate;
        }

        private void OnDisable() =>
            _triggerObserver.Entered -= Rotate;

        private async void Rotate(Collider player)
        {
            if (_targetAngleTransform == null)
                return;

            Vector3 directionToTarget = _targetAngleTransform.position - player.transform.position;

            float angle = Mathf.Atan2(directionToTarget.x, directionToTarget.z) * Mathf.Rad2Deg;
            await UniTask.WaitForSeconds(0.1f);

            while (_rotateCameraOnLastEnemyKilled.IsRotating)
            {
                await UniTask.Yield();
            }

            player.transform.DORotate(new Vector3(0, angle, 0), _rotateTime).SetUpdate(true);
            Transform cameraRotator = _cameraProvider.Get().Rotator;

            cameraRotator.DOLocalRotate(new Vector3(cameraRotator.eulerAngles.x, angle, cameraRotator.eulerAngles.z),
                _rotateTime).SetUpdate(true);
        }
    }
}