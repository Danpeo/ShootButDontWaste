using Platformer.Scripts.Entities.Enemies;

namespace Platformer.Scripts.State.WormStates;

public class WormStatePatrol(Fsm fsm, Worm worm) : WormState(fsm, worm)
{
    public override void Enter()
    {
    }

    public override void PhysicsProcess(double delta)
    {
        Worm.Patrol();
        if (Worm.IsAlmostAtCurrentTargetPosition())
        {
            Fsm.Set<WormStateIdle>();
        }
    }
}