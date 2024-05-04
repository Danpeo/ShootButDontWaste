using Godot;
using Platformer.Scripts.Constants;
using Platformer.Scripts.Constants.Animations;
using Platformer.Scripts.Entities;

namespace Platformer.Scripts.State.PlayerStates;

public class PlayerStateIdle : PlayerState
{
    public PlayerStateIdle(Fsm fsm, Player player) : base(fsm, player)
    {
    }

    public override void Exit()
    {
    }

    public override void PhysicsProcess(double delta)
    {
        TrySetJumpState();
        TrySetShootState();

        float direction = Input.GetAxis(InputBindings.MoveLeft, InputBindings.MoveRight);

        if (direction != 0)
        {
            Fsm.Set<PlayerStateMove>();
        }

        //string animatin = Player.IsHoldingObject() ? PlayerAnimation.Hit : PlayerAnimation.Idle;
       // string animatin = PlayerAnimation.Idle;

        TryPlayJump(PlayerAnim.Idle);
    }
}