using Platformer.Scripts.Entities.Enemies;

namespace Platformer.Scripts.State.WormStates;

public class WormStatePursue(Fsm fsm, Worm worm) : WormState(fsm, worm)
{
    public override void Enter()
    {
    }

    public override void PhysicsProcess(double delta)
    {
        if (Worm.IsPlayerInSpotArea())
            Worm.PursuePlayer();
        else
            Fsm.Set<WormStatePatrol>();
    }
}