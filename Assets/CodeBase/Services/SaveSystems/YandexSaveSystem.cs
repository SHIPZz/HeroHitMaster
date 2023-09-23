using Agava.YandexGames;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;

namespace CodeBase.Services.SaveSystems
{
    public class YandexSaveSystem : ISaveSystem
    {
        public void Save<T>(T data)
        {
            string jsonData = JsonConvert.SerializeObject(data);

            PlayerAccount.SetCloudSaveData(jsonData);
        }

        public async UniTask<T> Load<T>() where T : new()
        {
            object jsonData = null;
            
            PlayerAccount.GetCloudSaveData((data) => jsonData = JsonConvert.DeserializeObject<T>(data));

            while (jsonData is null)
            {
                await UniTask.Yield();
            }

            return (T)jsonData;
        }
    }
}