using System;
using System.Collections.Generic;

namespace CodeBase.Infrastructure
{
    public interface IGameStateMachine
    {
        void ChangeState<T>() where T : class, IState;
    }

    public class GameStateMachine : IGameStateMachine
    {
        private readonly Dictionary<Type, IState> _states = new();
        private IStateFactory _stateFactory;
        private IState _currentState;

        public GameStateMachine(IStateFactory stateFactory)
        {
            _stateFactory = stateFactory;
        }

        public void ChangeState<T>() where T : class, IState
        {
            (_currentState as IExit)?.Exit();
            
            IState state = _stateFactory.Create<T>();
            
            (state as IEnter)?.Enter();
            _currentState = state;
        }
    }
}