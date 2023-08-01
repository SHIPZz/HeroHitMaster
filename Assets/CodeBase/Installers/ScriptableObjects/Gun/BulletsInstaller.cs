using CodeBase.Services.Storages;
using UnityEngine;
using Zenject;

namespace CodeBase.Installers.ScriptableObjects.Gun
{
    [CreateAssetMenu(fileName = "BulletsInstaller", menuName = "Installers/BulletsInstaller")]
    public class BulletsInstaller : ScriptableObjectInstaller<BulletsInstaller>
    {
        [SerializeField] private BulletSettings _bulletSettings;
        [SerializeField] private BulletStaticDataService _bulletStaticDataService;
    
        public override void InstallBindings()
        {
            Container.BindInstance(_bulletSettings);
            Container.BindInstance(_bulletStaticDataService);
        }
    }
}