namespace CodeBase.Infrastructure
{
    public interface IPayloadedEnter<in T>
    {
        void Enter(T payload);
    }
}