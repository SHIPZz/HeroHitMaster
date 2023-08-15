using Cysharp.Threading.Tasks;

namespace CodeBase.Services.SaveSystems
{
    public interface ISaveSystem
    {
        void Save(WorldData worldData);
        UniTask<WorldData> Load();
    }
}