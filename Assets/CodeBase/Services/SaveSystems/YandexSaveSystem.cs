using Agava.YandexGames;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Services.SaveSystems
{
    public class YandexSaveSystem : ISaveSystem
    {
        private bool _isSaveDataReceived;
        private WorldData _worldData;

        public void Save(WorldData worldData)
        {
            string data = JsonUtility.ToJson(worldData);

            PlayerAccount.SetCloudSaveData(data);
        }

        public async UniTask<WorldData> Load()
        {
            PlayerAccount.GetCloudSaveData(OnSuccessCallback);

            while (_isSaveDataReceived == false)
            {
                await UniTask.Yield();
            }

            _isSaveDataReceived = false;
            return _worldData;
        }

        private WorldData ConvertJsonToGameData(string data) =>
            string.IsNullOrEmpty(data) ? new WorldData() : JsonUtility.FromJson<WorldData>(data);

        private void OnSuccessCallback(string data)
        {
            _worldData = ConvertJsonToGameData(data);
            _isSaveDataReceived = true;
        }
    }
}