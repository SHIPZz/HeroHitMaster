using CodeBase.Gameplay.Collision;
using Zenject;

namespace CodeBase.Installers.GameObjectInstallers.Bullet
{
    public class BulletInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            var triggerObserver =  GetComponent<TriggerObserver>();
            Container.Bind<TriggerObserver>().FromInstance(triggerObserver)
                .AsSingle();

            var bullet = GetComponent<Gameplay.Bullet.Bullet>();
            Container.Bind<Gameplay.Bullet.Bullet>().FromInstance(bullet)
                .AsSingle();

        }
    }
}