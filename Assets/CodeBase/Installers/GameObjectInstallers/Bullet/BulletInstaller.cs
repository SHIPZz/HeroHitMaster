using CodeBase.Gameplay.Bullet;
using CodeBase.Gameplay.Collision;
using UnityEngine;
using Zenject;

namespace CodeBase.Installers.GameObjectInstallers.Bullet
{
    public class BulletInstaller : MonoInstaller
    {
        [SerializeField] private TriggerObserver _triggerObserver;

        public override void InstallBindings()
        {
            Container.BindInstance(_triggerObserver);

            IBullet bullet = GetComponent<IBullet>();
            Container.Bind<IBullet>().FromInstance(bullet)
                .AsSingle();

        }
    }
}