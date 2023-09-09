using System.Collections.Generic;
using CodeBase.Services.Providers;
using UnityEngine;

namespace CodeBase.Services.Level
{
    public class LevelService : ILevelService
    {
        private readonly Dictionary<int, GameObject> _levels;
        private int _lastLevel;

        public LevelService(IProvider<LevelProvider> provider) => 
            _levels = provider.Get().Levels;

        public void Load(int level)
        {
            _levels[_lastLevel]?.SetActive(false);
            _lastLevel = level;
            _levels[level].SetActive(true);
        }
    }
}