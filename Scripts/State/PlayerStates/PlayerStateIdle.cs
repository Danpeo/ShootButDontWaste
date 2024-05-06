using DVar.ShootButDontWaste.Animations.AnimationTypes;
using DVar.ShootButDontWaste.Constants;
using Godot;
using Platformer.Scripts.Animations;
using Platformer.Scripts.Constants;
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

        float direction = Input.GetAxis(InputBindings.moveLeft, InputBindings.moveRight);

        if (direction != 0)
        {
            Fsm.Set<PlayerStateMove>();
        }

        //string animatin = Player.IsHoldingObject() ? PlayerAnimation.Hit : PlayerAnimation.Idle;
       // string animatin = PlayerAnimation.Idle;

        TryPlayJump(APlayer.Idle);
    }
}