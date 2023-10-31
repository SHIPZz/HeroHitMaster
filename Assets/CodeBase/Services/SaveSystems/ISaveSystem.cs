using CodeBase.Services.SaveSystems.Data;
using Cysharp.Threading.Tasks;

namespace CodeBase.Services.SaveSystems
{
    public interface ISaveSystem
    {
        void Save(WorldData data);

        UniTask<WorldData> Load();
    }
}