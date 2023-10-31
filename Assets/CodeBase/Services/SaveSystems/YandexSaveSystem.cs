﻿using System;
using Agava.YandexGames;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;

namespace CodeBase.Services.SaveSystems
{
    public class YandexSaveSystem : ISaveSystem
    {
        private string _data;
        private bool _isDataReceived;

        public void Save<WorldData>(WorldData data)
        {
            string jsonData = JsonConvert.SerializeObject(data);

            PlayerAccount.SetCloudSaveData(jsonData);
        }

        public async UniTask<WorldData> Load<WorldData>() where WorldData : new()
        {
            PlayerAccount.GetCloudSaveData(OnSuccessCallback);

            while (!_isDataReceived)
            {
                await UniTask.Yield();
            }

            if (String.IsNullOrEmpty(_data))
                return new WorldData();
            
            _isDataReceived = false;

            return JsonConvert.DeserializeObject<WorldData>(_data);
        }

        private void OnSuccessCallback(string data)
        {
            _data = data;
            _isDataReceived = true;
        }
    }
}