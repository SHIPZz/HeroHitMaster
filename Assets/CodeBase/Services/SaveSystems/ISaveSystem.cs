using Cysharp.Threading.Tasks;

namespace CodeBase.Services.SaveSystems
{
    public interface ISaveSystem
    {
        void Save<WorldData>(WorldData data);

        UniTask<WorldData> Load<WorldData>() where WorldData : new();
    }
}