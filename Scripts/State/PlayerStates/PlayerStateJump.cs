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

        Player.PlayAnimation(PlayerAnimation.Jump);
    }

    public override void PhysicsProcess(double delta)
    {
        TrySetShootState();

        Player.Jump();

        if (!Player.IsOnFloor())
        {
            Fsm.Set<PlayerStateIdle>();
        }
    }
}