using CodeBase.Services.Providers;
using DG.Tweening;

namespace CodeBase.Gameplay.Camera
{
    public class CameraZoomer
    {
        private readonly float _initalFieldOfView = 60;
        private readonly IProvider<UnityEngine.Camera> _cameraProvider;

        public CameraZoomer(IProvider<UnityEngine.Camera> cameraProvider) => 
            _cameraProvider = cameraProvider;

        public void Zoom(float targetValue, float targetDuration,float returnDuration, Ease returnEase) =>
            DOTween
                .To(() => _cameraProvider.Get().fieldOfView, x => _cameraProvider.Get().fieldOfView = x, targetValue, targetDuration)
                .OnComplete(() => 
                    DOTween
                        .To(() => _cameraProvider.Get().fieldOfView, x => _cameraProvider.Get().fieldOfView = x, _initalFieldOfView, returnDuration))
                .SetEase(returnEase);
    }
}