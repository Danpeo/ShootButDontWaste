using Platformer.Scripts.Constants.Times;
using Platformer.Scripts.Entities.Enemies;

namespace Platformer.Scripts.State.WormStates;

public class WormStatePrePursue(Fsm fsm, Worm worm) : WormState(fsm, worm)
{
    public override void Enter()
    {
        Stop(() => Fsm.Set<WormStatePursue>(), WormTime.PrePursue);
        Worm.Exclamation._Show();
    }

    public override void Exit()
    {
        Worm.Exclamation._Hide();
    }
}