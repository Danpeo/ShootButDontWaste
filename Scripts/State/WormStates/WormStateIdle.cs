using Platformer.Scripts.Entities.Enemies;

namespace Platformer.Scripts.State.WormStates;

public class WormStateIdle(Fsm fsm, Worm worm) : WormState(fsm, worm)
{
    public override void Enter()
    {
        Stop(() => Fsm.Set<WormStatePatrol>());
    }

    public override void Exit()
    {
        Worm.SwapTargetToOpposite();
    }
}