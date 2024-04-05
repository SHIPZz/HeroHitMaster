using Sirenix.OdinInspector;
using UnityEngine;

namespace CodeBase.ScriptableObjects.LevelDataSo
{
    [CreateAssetMenu(fileName = "LevelData", menuName = "Gameplay/Data/Level data")]
    public class LevelData : SerializedScriptableObject
    {
        public string Id => name;
        
        public int Reward;
    }
}