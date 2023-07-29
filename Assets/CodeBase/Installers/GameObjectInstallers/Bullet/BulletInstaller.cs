using CodeBase.Gameplay.Collision;
using Gameplay;
using Gameplay.Bullet;
using UnityEngine;
using Zenject;

namespace Installers.GameObjectInstallers.Bullet
{
    public class BulletInstaller : MonoInstaller
    {
        [SerializeField] private Gameplay.Bullet.Bullet _bullet;
        [SerializeField] private TriggerObserver _triggerObserver;

        public override void InstallBindings()
        {
            Container.BindInstance(_triggerObserver);
            
            Container.BindInterfacesAndSelfTo<IBullet>().FromInstance(_bullet)
                .AsSingle();

        }
    }
}