using Platformer.Scripts.Entities.Enemies;

namespace Platformer.Scripts.State.MushroomStates;

public class MushroomStateLosePlayer : MushroomState
{
    public MushroomStateLosePlayer(Fsm fsm, Mushroom mushroom) : base(fsm, mushroom)
    {
    }

    public override void Enter()
    {
        Mushroom.Idle();
    }
}