using CodeBase.Gameplay.Bullet;
using CodeBase.Gameplay.Collision;
using UnityEngine;
using Zenject;

namespace CodeBase.Installers.GameObjectInstallers.Bullet
{
    public class BulletInstaller : MonoInstaller
    {
        [SerializeField] private CodeBase.Gameplay.Bullet.Bullet _bullet;
        [SerializeField] private TriggerObserver _triggerObserver;

        public override void InstallBindings()
        {
            Container.BindInstance(_triggerObserver);
            
            Container.BindInterfacesAndSelfTo<IBullet>().FromInstance(_bullet)
                .AsSingle();

        }
    }
}