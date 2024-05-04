using Godot;
using Platformer.Scripts.Animations;
using Platformer.Scripts.Entities;

namespace Platformer.Scripts.State.PlayerStates;

public class PlayerStateMove : PlayerState
{
    public PlayerStateMove(Fsm fsm, Player player) : base(fsm, player)
    {
    }

    public override void Enter()
    {
        base.Enter();

        Player.PlayAnimation(PlayerAnim.Run);
    }

    public override void PhysicsProcess(double delta)
    {
        TrySetJumpState();
        TryPlayJump(PlayerAnim.Run);
        TrySetShootState();

        Player.Move();

        if (Player.Direction == 0)
        {
            Fsm.Set<PlayerStateIdle>();
        }
    }
}