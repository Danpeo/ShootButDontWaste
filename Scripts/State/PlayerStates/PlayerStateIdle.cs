using Godot;
using Platformer.Scripts.Constants.Animations;
using Platformer.Scripts.Entities;
using PlayerInput = Platformer.Scripts.Constants.PlayerInput;

namespace Platformer.Scripts.State.PlayerStates;

public class PlayerStateIdle : PlayerState
{
    public PlayerStateIdle(Fsm fsm, Player player) : base(fsm, player)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Player.PlayAnimation(PlayerAnimation.Idle);
    }

    public override void Exit()
    {
    }

    public override void PhysicsProcess(double delta)
    {
        TrySetJumpState();
        TrySetShootState();

        float direction = Input.GetAxis(PlayerInput.MoveLeft, PlayerInput.MoveRight);

        if (direction != 0)
        {
            Fsm.Set<PlayerStateMove>();
        }
        
        TryPlayJump(PlayerAnimation.Idle);
    }
}