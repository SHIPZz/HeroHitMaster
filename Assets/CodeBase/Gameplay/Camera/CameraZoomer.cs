using CodeBase.Services.Providers;
using DG.Tweening;

namespace CodeBase.Gameplay.Camera
{
    public class CameraZoomer
    {
        private readonly float _initalFieldOfView = 60;
        private readonly IProvider<CameraData> _cameraProvider;

        public CameraZoomer(IProvider<CameraData> cameraProvider) => 
            _cameraProvider = cameraProvider;

        public void Zoom(float targetValue, float targetDuration,float returnDuration, Ease returnEase) =>
            DOTween
                .To(() => _cameraProvider.Get().Camera.fieldOfView, x => _cameraProvider.Get().Camera.fieldOfView = x, targetValue, targetDuration)
                .OnComplete(() => 
                    DOTween
                        .To(() => _cameraProvider.Get().Camera.fieldOfView, x => _cameraProvider.Get().Camera.fieldOfView = x, _initalFieldOfView, returnDuration))
                .SetEase(returnEase);
    }
}