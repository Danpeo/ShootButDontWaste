using Godot;
using Platformer.Scripts.Constants.Animations;
using Platformer.Scripts.Entities;
using PlayerInput = Platformer.Scripts.Constants.PlayerInput;

namespace Platformer.Scripts.State.PlayerStates;

public class PlayerStateMove : PlayerState
{
    public PlayerStateMove(Fsm fsm, Player player) : base(fsm, player)
    {
    }

    public override void Enter()
    {
        base.Enter();

        Player.PlayerAnimator.Play(PlayerAnimation.Run);
    }

    public override void Update(double delta)
    {
        TrySetJumpState();
        TryPlayJump(PlayerAnimation.Run);

        float direction = Input.GetAxis(PlayerInput.MoveLeft, PlayerInput.MoveRight);
        Player.Direction = direction;

        if (direction == 0)
        {
            Fsm.Set<PlayerStateIdle>();
        }
    }
}