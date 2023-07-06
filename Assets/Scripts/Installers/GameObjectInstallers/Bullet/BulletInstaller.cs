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
            Container.BindInterfacesAndSelfTo<IBullet>().FromInstance(_bullet)
                .AsSingle();

            Container.BindInstance(_triggerObserver);
        }
    }
}