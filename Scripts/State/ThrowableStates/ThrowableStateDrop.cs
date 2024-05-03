using Platformer.Scripts.Properties.Interfaces;

namespace Platformer.Scripts.State.ThrowableStates;

public class ThrowableStateDrop : ThrowableState
{
    public ThrowableStateDrop(Fsm fsm, IThrowable throwable) : base(fsm, throwable)
    {
    }

    public override void Enter()
    {
        Throwable.Drop();
        Fsm.Set<ThrowableStateIdle>();
    }
}