using DVar.ShootButDontWaste.Constants;
using Platformer.Scripts.Constants.Times;
using Platformer.Scripts.Entities.Enemies;
using Platformer.Scripts.Utils;

namespace Platformer.Scripts.State.MushroomStates;

public class MushroomStateHit : MushroomState
{
    private readonly EasyTimer _hitTimer;
    
    public MushroomStateHit(Fsm fsm, Mushroom mushroom) : base(fsm, mushroom)
    {
        _hitTimer = new EasyTimer(Mushroom, () => Fsm.Set<MushroomStateIdle>(), MushroomTime.Hit);
    }
    
    public override void Enter()
    {
        _hitTimer.Start();
        Mushroom.Hit();
    }

    public override void Exit()
    {
        _hitTimer.Stop();
    }
}