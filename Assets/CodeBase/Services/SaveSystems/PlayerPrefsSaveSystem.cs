using CodeBase.Services.SaveSystems.Data;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;

namespace CodeBase.Services.SaveSystems
{
    public class PlayerPrefsSaveSystem : ISaveSystem
    {
        public void Save(WorldData data)
        {
            string jsonData = JsonConvert.SerializeObject(data);
            PlayerPrefs.SetString(typeof(WorldData).FullName, jsonData);
            PlayerPrefs.Save();
        }

        public async UniTask<WorldData> Load() 
        {
            if (PlayerPrefs.HasKey(typeof(WorldData).FullName))
            {
                string jsonData = PlayerPrefs.GetString(typeof(WorldData).FullName);
                return JsonConvert.DeserializeObject<WorldData>(jsonData);
            }

            await UniTask.Yield();

            return new WorldData();
        }
    }
}