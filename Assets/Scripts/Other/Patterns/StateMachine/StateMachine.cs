using System.Collections.Generic;
using UnityEngine;

namespace FSM
{
    public abstract class StateMachine<T> : SingletonMB<T> where T : MonoBehaviour
    {
        public Stack<State<T>> Stack = new Stack<State<T>>();
        public Dictionary<string, State<T>> StatesById = new Dictionary<string, State<T>>();

        public void RegisterState(State<T> state, T fsm)
        {
            var stateTypeAsString = state.GetType().ToString();
            StatesById[stateTypeAsString] = state;
            state.FSM = fsm;
        }

        public void InitializeAllStates()
        {
            foreach (var state in StatesById.Values)
                state.Initialize();
        }

        public T1 GetState<T1>() where T1 : State<T>
        {
            var stateAsString = typeof(T1).ToString();
            return StatesById[stateAsString] as T1;
        }

        public void PushState<T1>() where T1 : State<T>
        {
            var stateAsString = typeof(T1).ToString();
            var state = StatesById[stateAsString];
            PushState(state);
        }

        public void PushState(State<T> state)
        {
            if (Stack.Count > 0)
            {
                var currentState = Stack.Peek();
                currentState.OnExitState();
            }

            Stack.Push(state);
            state.OnEnterState();
        }

        public State<T> PeekState()
        {
            State<T> currentState = null;

            if (Stack.Count > 0)
                currentState = Stack.Peek();

            return currentState;
        }

        public void PopState()
        {
            if (Stack.Count > 0)
            {
                var state = Stack.Pop();
                state.OnExitState();
            }

            if (Stack.Count > 0)
            {
                var currentState = Stack.Peek();

                currentState.OnEnterState();
            }
        }
    }
}