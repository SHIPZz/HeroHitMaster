using UnityEngine;

namespace CodeBase.Services.Providers
{
    public class CameraProvider : IProvider<Camera>
    {
        private Camera _camera;
        
        public Camera Get() => 
            _camera;

        public void Set(Camera camera) => 
            _camera = camera;
    }
}