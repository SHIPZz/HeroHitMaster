namespace CodeBase.Infrastructure
{
    public interface IPayloadedState<TPayload> : IExit
    {
        void Enter(TPayload payload);
    }
}