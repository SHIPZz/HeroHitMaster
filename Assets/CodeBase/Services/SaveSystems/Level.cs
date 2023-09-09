using UnityEngine;

namespace CodeBase.Services.SaveSystems
{
    public class Level : MonoBehaviour
    {
        [field: SerializeField] public int Reward { get; private set; }
    }
}