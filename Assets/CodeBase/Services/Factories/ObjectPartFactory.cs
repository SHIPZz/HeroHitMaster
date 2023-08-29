using System.Collections.Generic;
using System.Linq;
using CodeBase.Gameplay.ObjectBodyPart;
using UnityEngine;
using Zenject;

namespace CodeBase.Services.Factories
{
    public class ObjectPartFactory
    {
        private readonly DiContainer _diContainer;
        private readonly Dictionary<DestroyableObjectTypeId, DestroyableObjectBodyPart> _destroyableObjectParts;

        public ObjectPartFactory(DiContainer diContainer)
        {
            _diContainer = diContainer;
            _destroyableObjectParts = Resources.LoadAll<DestroyableObjectBodyPart>("Prefabs/ObjectPart")
                .ToDictionary(x => x.DestroyableObjectTypeId, x => x);
        }

        public DestroyableObjectBodyPart CreateBy(DestroyableObjectTypeId destroyableObjectTypeId, Vector3 at)
        {
            var prefab = _destroyableObjectParts[destroyableObjectTypeId];
            return _diContainer
                .InstantiatePrefabForComponent<DestroyableObjectBodyPart>(prefab, at, Quaternion.identity, null);
        }
    }
}