using Platformer.Scripts.Constants.Animations;
using Platformer.Scripts.Entities;

namespace Platformer.Scripts.State.PlayerStates;

public class PlayerStateJump : PlayerState
{
    public PlayerStateJump(Fsm fsm, Player player) : base(fsm, player)
    {
    }

    public override void Enter()
    {
        base.Enter();

        Player.PlayerAnimator.Play(PlayerAnimation.Jump);
    }

    public override void Update(double delta)
    {
        Player.Velocity = Player.Velocity with { Y = Player.JumpSpeed };

        if (!Player.IsOnFloor())
        {
            Fsm.Set<PlayerStateIdle>();
        }
    }
}