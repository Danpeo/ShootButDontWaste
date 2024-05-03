using Platformer.Scripts.Properties.Interfaces;

namespace Platformer.Scripts.State.ThrowableStates;

public class ThrowableStateThrow : ThrowableState
{
    public ThrowableStateThrow(Fsm fsm, IThrowable throwable) : base(fsm, throwable)
    {
    }

    public override void Enter()
    {
        Throwable.Throw();
        Fsm.Set<ThrowableStateIdle>();
    }
}