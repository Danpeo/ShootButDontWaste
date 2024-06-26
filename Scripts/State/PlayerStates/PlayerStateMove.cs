using DVar.ShootButDontWaste.Animations.AnimationTypes;
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

        Player.PlayAnimation(APlayer.Run);
    }

    public override void PhysicsProcess(double delta)
    {
        TrySetJumpState();
        TryPlayJump(APlayer.Run);
        TrySetShootState();

        Player.Move();

        if (Player.DirectionX == 0)
        {
            Fsm.Set<PlayerStateIdle>();
        }
    }
}