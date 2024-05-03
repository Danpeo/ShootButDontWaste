using Platformer.Scripts.Properties.Interfaces;

namespace Platformer.Scripts.State.ThrowableStates;

public class ThrowableState : FsmState
{
    protected readonly IThrowable Throwable;
    
    public ThrowableState(Fsm fsm, IThrowable throwable) : base(fsm)
    {
        Throwable = throwable;
    }
}