using System.Collections.Generic;
using Enums;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

namespace CodeBase.Gameplay.EnemyBodyParts
{
    public class EnemyBodyPartStorage : SerializedMonoBehaviour
    {
        [OdinSerialize] private Dictionary<EnemyTypeId, EnemyBodyPart> _enemyBodyPartViews;

        public EnemyBodyPart Get(EnemyTypeId enemyTypeId) => 
            _enemyBodyPartViews[enemyTypeId];

        public List<EnemyBodyPart> GetAll()
        {
            var enemyBodyParts = new List<EnemyBodyPart>();

            foreach (var enemyBodyPartView in _enemyBodyPartViews.Values)
            {
                enemyBodyParts.Add(enemyBodyPartView);
            }

            return enemyBodyParts;
        }
    }
}