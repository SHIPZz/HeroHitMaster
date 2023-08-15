using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Services.SaveSystems
{
    public class PlayerPrefsSaveSystem : ISaveSystem
    {
        private const string DataKey = "PlayerProgress";
        
        public void Save(WorldData worldData)
        {
            string data = JsonUtility.ToJson(worldData);
            PlayerPrefs.SetString(DataKey, data);
            PlayerPrefs.Save();
        }

        public async UniTask<WorldData> Load()
        {
            if (PlayerPrefs.HasKey(DataKey))
            {
                string data = PlayerPrefs.GetString(DataKey);
                var playerProgress = JsonUtility.FromJson<WorldData>(data);
                return playerProgress;
            }

            await UniTask.Yield();

            return new WorldData();
        }
    }
}