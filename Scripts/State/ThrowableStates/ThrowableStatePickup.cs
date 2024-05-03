using Godot;
using Platformer.Scripts.Constants;
using Platformer.Scripts.Properties.Interfaces;

namespace Platformer.Scripts.State.ThrowableStates;

public class ThrowableStatePickup : ThrowableState
{
    public ThrowableStatePickup(Fsm fsm, IThrowable throwable) : base(fsm, throwable)
    {
    }

    public override void Enter()
    {
    }

    public override void PhysicsProcess(double delta)
    {
        Throwable.Pickup();

        if (!Throwable.IsPlayerInArea() && Throwable.IsHeld)
        {
            Fsm.Set<ThrowableStateIdle>();
        }

        if (Throwable.IsHeld && Input.IsActionJustPressed(InputBindings.Throw))
        {
            Fsm.Set<ThrowableStateThrow>();
        }

        if (Throwable.IsHeld && Input.IsActionJustReleased(InputBindings.Interact))
        {
            Fsm.Set<ThrowableStateDrop>();
        }
    }
}