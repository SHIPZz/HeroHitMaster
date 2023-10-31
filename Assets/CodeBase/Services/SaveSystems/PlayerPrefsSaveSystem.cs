using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;

namespace CodeBase.Services.SaveSystems
{
    public class PlayerPrefsSaveSystem : ISaveSystem
    {
        public void Save<WorldData>(WorldData data)
        {
            string jsonData = JsonConvert.SerializeObject(data);
            PlayerPrefs.SetString(nameof(Data.WorldData), jsonData);
            PlayerPrefs.Save();
        }

        public async UniTask<WorldData> Load<WorldData>() where WorldData : new()
        {
            if (PlayerPrefs.HasKey(nameof(Data.WorldData)))
            {
                string jsonData = PlayerPrefs.GetString(nameof(Data.WorldData));
                return JsonConvert.DeserializeObject<WorldData>(jsonData);
            }

            await UniTask.Yield();

            return new WorldData();
        }
    }
}