using CodeBase.Gameplay.MaterialChanger;
using CodeBase.Services.Factories;
using UnityEngine;
using Zenject;

namespace CodeBase.Gameplay.ObjectBodyPart
{
    [RequireComponent(typeof(IMaterialChanger))]
    public class DestroyableObjectBodyPartPositionSetter : MonoBehaviour
    {
        [SerializeField] private MeshMaterialChanger _materialChanger;
        
        private ObjectPartFactory _objectPartFactory;
        private bool _isDisabled;

        [Inject]
        public void Construct(ObjectPartFactory objectPartFactory) =>
            _objectPartFactory = objectPartFactory;

        private void OnEnable() =>
            _materialChanger.StartedChanged += Disable;

        private void Disable() =>
            _isDisabled = true;

        public void Set(DestroyableObjectTypeId destroyableObjectTypeId, DestroyableObject destroyableObject)
        {
            if (_isDisabled)
                return;

            DestroyableObjectBodyPart destroyableParts =
                _objectPartFactory.CreateBy(destroyableObjectTypeId, destroyableObject.transform.position);
            destroyableParts.Enable();
        }
    }
}