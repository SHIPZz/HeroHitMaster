using CodeBase.Gameplay.MaterialChanger;
using CodeBase.Gameplay.ObjectBodyPart;
using UnityEngine;
using Zenject;

namespace CodeBase.Installers.GameObjectInstallers.DestroyableObject
{
    public class DestroyableObjectInstaller : MonoInstaller
    {
        [SerializeField] private Gameplay.ObjectBodyPart.DestroyableObject _destroyableObject;
        [SerializeField] private MeshMaterialChanger _meshMaterialChanger;
    
        public override void InstallBindings()
        {
            Container.BindInstance(_destroyableObject);
            Container.BindInstance(_destroyableObject.DestroyableObjectTypeId);
            Container.Bind<IMaterialChanger>().To<MeshMaterialChanger>().FromInstance(_meshMaterialChanger);
            Container.BindInterfacesAndSelfTo<DestroyableObjectPartMediator>().AsSingle();
            Container.Bind<DestroyableObjectPartsActivator>().AsSingle();
            Container.Bind<DestroyableObjectBodyPartPositionSetter>().AsSingle();
            Container.BindInterfacesAndSelfTo<DestroyableObjectPartActivatorDisabler>().AsSingle();
        }
    }
}
