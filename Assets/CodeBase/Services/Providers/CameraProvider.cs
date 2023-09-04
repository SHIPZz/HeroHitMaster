using CodeBase.Gameplay.Camera;
using UnityEngine;

namespace CodeBase.Services.Providers
{
    public class CameraProvider : IProvider<Camera>, IProvider<CameraData>
    {
        private Camera _camera;
        private CameraData _cameraData;
        
        public Camera Get() => 
            _camera;

        public void Set(CameraData cameraData) => 
            _cameraData = cameraData;

        public void Set(Camera camera) => 
            _camera = camera;

        CameraData IProvider<CameraData>.Get() => 
        _cameraData;
    }
}