namespace CodeBase.Infrastructure
{
    public interface IStateFactory
    {
        IState Create<T>() where T : class, IState;
    }
}