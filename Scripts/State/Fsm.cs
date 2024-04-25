using System;
using System.Collections.Generic;

namespace Platformer.Scripts.State;

public class Fsm
{
    public FsmState? CurrentState { get; private set; }
    private Dictionary<Type, FsmState> _states = new();

    public void Add(FsmState state)
    {
        _states.Add(state.GetType(), state);
    }

    public void Set<T>() where T : FsmState
    {
        var type = typeof(T);
        if (CurrentState?.GetType() == type)
        {
            return;
        }

        if (_states.TryGetValue(type, out var newState))
        {
            CurrentState?.Exit();
            CurrentState = newState;
            CurrentState.Enter();
        }
    }

    public void Update(double delta)
    {
        CurrentState?.Update(delta);
    }
}