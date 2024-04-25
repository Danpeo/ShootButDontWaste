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

        Player.PlayAnimation(PlayerAnimation.Run);
    }

    public override void PhysicsProcess(double delta)
    {
        TrySetJumpState();
        TryPlayJump(PlayerAnimation.Run);
        TrySetShootState();

        Player.Move();

        if (Player.Direction == 0)
        {
            Fsm.Set<PlayerStateIdle>();
        }
    }
}