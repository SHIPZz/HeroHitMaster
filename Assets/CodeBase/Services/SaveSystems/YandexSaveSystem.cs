using System;
using Agava.YandexGames;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;

namespace CodeBase.Services.SaveSystems
{
    public class YandexSaveSystem : ISaveSystem
    {
        private string _data;
        private bool _isDataReceived;

        public void Save<T>(T data)
        {
            string jsonData = JsonConvert.SerializeObject(data);

            PlayerAccount.SetCloudSaveData(jsonData);
        }
        
        public async UniTask<T> Load<T>() where T : new()
        {
            PlayerAccount.GetCloudSaveData(OnSuccessCallback);

            while (!_isDataReceived)
            {
                await UniTask.Yield();
            }

            if (String.IsNullOrEmpty(_data))
                return new T();
            
            _isDataReceived = false;

            return JsonConvert.DeserializeObject<T>(_data);
        }

        private void OnSuccessCallback(string data)
        {
            _data = data;
            _isDataReceived = true;
        }
    }
}