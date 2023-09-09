using Cysharp.Threading.Tasks;

namespace CodeBase.Services.SaveSystems
{
    public interface ISaveSystem
    {
        void Save<T>(T data);

        UniTask<T> Load<T>() where T : new();
    }
}