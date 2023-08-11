namespace CodeBase.Services.Providers
{
    public interface IProvider<T> where T : class
    {
        T Get();
        void Set(T camera);
    }
}