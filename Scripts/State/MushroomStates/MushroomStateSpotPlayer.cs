using Platformer.Scripts.Entities.Enemies;

namespace Platformer.Scripts.State.MushroomStates;

public class MushroomStateSpotPlayer : MushroomState
{
    public MushroomStateSpotPlayer(Fsm fsm, Mushroom mushroom) : base(fsm, mushroom)
    {
    }

    public override void Enter()
    {
        Mushroom.Showup();
    }
}