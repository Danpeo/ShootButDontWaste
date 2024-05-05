using Platformer.Scripts.Entities.Enemies;

namespace Platformer.Scripts.State.MushroomStates;

public class MushroomStateIdle : MushroomState
{
    public MushroomStateIdle(Fsm fsm, Mushroom mushroom) : base(fsm, mushroom)
    {
    }

    public override void PhysicsProcess(double delta)
    {
        Mushroom.Idle();
    }
}