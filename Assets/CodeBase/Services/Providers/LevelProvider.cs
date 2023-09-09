using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace CodeBase.Services.Providers
{
    public class LevelProvider : SerializedMonoBehaviour, IProvider<LevelProvider>
    {
        [OdinSerialize] public  Dictionary<int, GameObject> Levels { get; private set; }

        public LevelProvider Get() => 
        this;

        public void Set(LevelProvider t)
        {
        }
    }
}