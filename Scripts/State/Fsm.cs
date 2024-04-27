using System;
using System.Collections.Generic;

namespace Platformer.Scripts.State;

public class Fsm
{
    public FsmState? CurrentState { get; private set; }
    private readonly Dictionary<Type, FsmState> _states = new();

    public void Add<TState>(TState state) where TState : FsmState
    {
        _states.Add(state.GetType(), state);
    }

    public void Set<TState>() where TState : FsmState
    {
        var type = typeof(TState);
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

    public void PhysicsProcess(double delta)
    {
        CurrentState?.PhysicsProcess(delta);
    }
}