using CodeBase.Gameplay.Weapons;
using CodeBase.Services.Providers;
using UnityEngine;
using Zenject;

namespace CodeBase.Gameplay.Camera
{
    public class CameraShakeMediator : MonoBehaviour
    {
        [SerializeField] private CameraShake _cameraShake;
        
        private IProvider<Weapon> _provider;
        private Weapon _weapon;

        [Inject]
        private void Construct(IProvider<Weapon> provider)
        {
            _provider = provider;
        }
        
        public void Init()
        {
            _weapon = _provider.Get();
            _weapon.Shooted += _cameraShake.MakeShake;
        }
    }
}