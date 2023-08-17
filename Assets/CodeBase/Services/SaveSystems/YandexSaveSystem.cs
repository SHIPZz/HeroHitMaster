using Agava.YandexGames;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;

namespace CodeBase.Services.SaveSystems
{
    public class YandexSaveSystem : ISaveSystem
    {
        private bool _isSaveDataReceived;
        private object _jsonData;

        public void Save<T>(T data)
        {
            string jsonData = JsonConvert.SerializeObject(data);

            PlayerAccount.SetCloudSaveData(jsonData);
        }

        public async UniTask<T> Load<T>() where T : new()
        {
            PlayerAccount.GetCloudSaveData(OnSuccessCallback<T>);

            while (!_isSaveDataReceived)
            {
                await UniTask.Yield();
            }

            _isSaveDataReceived = false;
            return (T)_jsonData;
        }

        private void OnSuccessCallback<T>(string data)
        {
            _jsonData = JsonConvert.DeserializeObject<T>(data);
            _isSaveDataReceived = true;
        }
    }
}