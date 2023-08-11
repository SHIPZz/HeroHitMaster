using System;

namespace CodeBase.Services.Providers
{
    public interface IProvider<T> where T : class
    {
        T Get();
        void Set(T t);
    }

    public interface IProvider<TIn, TOut> where TIn : Enum
    {
        TOut Get(TIn id);
    }
}