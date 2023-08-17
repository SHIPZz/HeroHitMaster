using CodeBase.Services.SaveSystems.Sound;
using Cysharp.Threading.Tasks;

namespace CodeBase.Services.SaveSystems
{
    public interface ISaveSystem
    {
        void Save<T>(T data);

        UniTask<T> Load<T>() where T : new();
        // void Save(WorldData worldData);
        // UniTask<WorldData> Load();
    }
}