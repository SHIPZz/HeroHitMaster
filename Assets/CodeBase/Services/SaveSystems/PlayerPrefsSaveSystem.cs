using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;

namespace CodeBase.Services.SaveSystems
{
    public class PlayerPrefsSaveSystem : ISaveSystem
    {
        private const string DataKey = "Data";
        
        public void Save<T>(T data)
        {
            string jsonData = JsonConvert.SerializeObject(data);
            PlayerPrefs.SetString(typeof(T).FullName, jsonData);
            PlayerPrefs.Save();
        }

        public async UniTask<T> Load<T>() where T : new()
        {
            if (PlayerPrefs.HasKey(typeof(T).FullName))
            {
                string jsonData = PlayerPrefs.GetString(typeof(T).FullName);
                return JsonConvert.DeserializeObject<T>(jsonData);
            }

            await UniTask.Yield();

            return new T();
        }
    }
}