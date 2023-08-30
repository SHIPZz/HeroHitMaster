using System.Collections.Generic;
using System.Linq;
using CodeBase.Enums;
using UnityEngine;
using Zenject;

namespace CodeBase.Gameplay.EnemyBodyParts
{
    public class EnemyPartFactory
    {
        private readonly Dictionary<EnemyTypeId, EnemyBodyPart> _parts;
        private DiContainer _diContainer;

        public EnemyPartFactory(DiContainer diContainer)
        {
            _diContainer = diContainer;
            _parts = Resources.LoadAll<EnemyBodyPart>("Prefabs/EnemyBodyPart")
                .ToDictionary(x => x.EnemyTypeId, x => x);
        }

        public EnemyBodyPart CreateBy(EnemyTypeId enemyTypeId, Vector3 at)
        {
            EnemyBodyPart enemyPart = _parts[enemyTypeId];
            return _diContainer
                .InstantiatePrefabForComponent<EnemyBodyPart>(enemyPart, at, Quaternion.identity, null);
        }
    }
}