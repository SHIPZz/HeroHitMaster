using CodeBase.Enums;
using UnityEngine;

namespace CodeBase.Installers.ScriptableObjects.Player
{
    [CreateAssetMenu(fileName = "PlayerData", menuName = "Gameplay/PlayerData")]
    public class PlayerData : ScriptableObject
    {
        public PlayerTypeId PlayerTypeId;
        
        [Range(100, 200)] public int Hp = 100;
        
        [Range(3, 8)] public float Speed = 3;
    }
}