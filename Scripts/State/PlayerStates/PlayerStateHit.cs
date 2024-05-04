using Platformer.Scripts.Constants.Times;
using Platformer.Scripts.Entities;
using Platformer.Scripts.Utils;

namespace Platformer.Scripts.State.PlayerStates;

public class PlayerStateHit : PlayerState
{
    private readonly EasyTimer _hitTimer;

    public PlayerStateHit(Fsm fsm, Player player) : base(fsm, player)
    {
        _hitTimer = new EasyTimer(Player, () => Fsm.Set<PlayerStateIdle>(), PlayerTime.Hit);
    }

    public override void Enter()
    {
        _hitTimer.Start();
        Player.Hit((float)_hitTimer.WaitTime);
    }

    public override void Exit()
    {
        _hitTimer.Stop();
        Player.Direction = 0;
    }
}