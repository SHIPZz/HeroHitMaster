using CodeBase.Services.SaveSystems;
using CodeBase.Services.SaveSystems.Data;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Services.Providers
{
    public class WorldDataService : IWorldDataService
    {
        private readonly ISaveSystem _saveSystem;

        public WorldData WorldData { get; private set; }

        public void Reset()
        {
            WorldData = new WorldData();
            Save();
        }

        public WorldDataService(ISaveSystem saveSystem) =>
            _saveSystem = saveSystem;

        public async UniTask Load()
        {
            WorldData = await _saveSystem.Load();
        }

        public void Save() =>
            _saveSystem.Save(WorldData);
    }

    public interface IWorldDataService
    {
        UniTask Load();
        void Save();
        WorldData WorldData { get; }
        void Reset();
    }
}