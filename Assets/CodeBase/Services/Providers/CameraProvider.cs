using CodeBase.Gameplay.Camera;

namespace CodeBase.Services.Providers
{
    public class CameraProvider : IProvider<CameraData>
    {
        private CameraData _cameraData;

        public void Set(CameraData cameraData) =>
            _cameraData = cameraData;
        
        CameraData IProvider<CameraData>.Get() =>
            _cameraData;
    }
}