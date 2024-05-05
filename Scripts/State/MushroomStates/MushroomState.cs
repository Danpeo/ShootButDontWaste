using Platformer.Scripts.Entities.Enemies;

namespace Platformer.Scripts.State.MushroomStates;

public class MushroomState : FsmState
{
    protected readonly Mushroom Mushroom;
    
    public MushroomState(Fsm fsm, Mushroom mushroom) : base(fsm)
    {
        Mushroom = mushroom;
        Mushroom.Ready += () => Mushroom.OnSpottedPlayer(() => Fsm.Set<MushroomStateSpotPlayer>());
        Mushroom.Ready += () => Mushroom.OnLosedPlayer(() => Fsm.Set<MushroomStateLosePlayer>());
    }
}