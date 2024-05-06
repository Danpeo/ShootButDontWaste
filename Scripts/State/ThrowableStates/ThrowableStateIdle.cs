using DVar.ShootButDontWaste.Constants;
using Godot;
using Platformer.Scripts.Constants;
using Platformer.Scripts.Properties.Interfaces;

namespace Platformer.Scripts.State.ThrowableStates;

public class ThrowableStateIdle : ThrowableState
{
    public ThrowableStateIdle(Fsm fsm, IThrowable throwable) : base(fsm, throwable)
    {
    }

    public override void PhysicsProcess(double delta)
    {
        if (Input.IsActionJustReleased(InputBindings.interact))
        {
            Fsm.Set<ThrowableStatePickup>();
        }
    }
}