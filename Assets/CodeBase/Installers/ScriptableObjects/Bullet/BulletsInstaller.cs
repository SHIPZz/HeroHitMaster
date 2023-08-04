using CodeBase.ScriptableObjects.Bullet;
using UnityEngine;
using Zenject;

namespace CodeBase.Installers.ScriptableObjects.Bullet
{
    [CreateAssetMenu(fileName = "BulletsInstaller", menuName = "Installers/BulletsInstaller")]
    public class BulletsInstaller : ScriptableObjectInstaller<BulletsInstaller>
    {
        [SerializeField] private BulletSettings _bulletSettings;
    
        public override void InstallBindings()
        {
            Container.BindInstance(_bulletSettings);
        }
    }
}