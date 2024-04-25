using Platformer.Scripts.Constants.Animations;
using Platformer.Scripts.Constants.Times;
using Platformer.Scripts.Effects;
using Platformer.Scripts.Entities;
using Platformer.Scripts.Utils;

namespace Platformer.Scripts.State.PlayerStates;

public class PlayerStateHit : PlayerState
{
    private EasyTimer? _hitTimer;

    public PlayerStateHit(Fsm fsm, Player player) : base(fsm, player)
    {
    }

    public override void Enter()
    {
        _hitTimer = new EasyTimer(Player, () => Fsm.Set<PlayerStateIdle>(), PlayerTime.Hit);

        _hitTimer.Start();
        Player.Direction = 0;
        Player.PlayerAnimator.Play(PlayerAnimation.Hit);
        Player.Autoload<FrameFreeze>("FrameFreeze").Activate(0.05f, (float)_hitTimer.Timer.WaitTime);
    }

    public override void Exit()
    {
        _hitTimer!.Stop();
    }
}