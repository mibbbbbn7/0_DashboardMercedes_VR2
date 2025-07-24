using System.Collections.Generic;

namespace DashboardMercedes {
    public class FSM<TState> where TState : IState
    {
        protected Dictionary<string, TState> _mystatesDictionary = new();

        protected TState _currentState;

        public void AddState(string stateName, TState state)
        {
            _mystatesDictionary[stateName] = state;
        }

        public void RemoveState(string stateName)
        {
            if (_mystatesDictionary.ContainsKey(stateName))
            {
                _mystatesDictionary.Remove(stateName);
            }
        }

        public void GoTo(string newState)
        {
            if (_currentState != null)
            {
                _currentState.ExitState();
            }

            _currentState = _mystatesDictionary[newState];

            _currentState.EnterState();
        }

        public void UpdateState()
        {
            if(_currentState != null)
            {
                _currentState.UpdateState();
            }
        }
    }
}
