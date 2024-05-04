using Godot;
using Platformer.Scripts.Animations;
using Platformer.Scripts.Constants;
using Platformer.Scripts.Entities;
using Platformer.Scripts.Utils;

namespace Platformer.Scripts.State.PlayerStates;

public class PlayerState : FsmState
{
    protected readonly Player Player;

    public PlayerState(Fsm fsm, Player player) : base(fsm)
    {
        Player = player;
        Player.Ready += () => Player.OnAmmoReducedByDamage(() => Fsm.Set<PlayerStateHit>());
    }

    public override void Enter()
    {
    }

    public override void PhysicsProcess(double delta)
    {
    }

    protected void TryPlayJump(PlayerAnim stateAnimation)
    {
        Player.PlayAnimation(Player.Velocity.Y < 0 ? PlayerAnim.Jump : stateAnimation);
    }

    protected void TrySetJumpState()
    {
        if (InputExt.IsActionHolding(InputBindings.Jump) && Player.IsOnFloor())
            Fsm.Set<PlayerStateJump>();
    }
    
    protected void TrySetShootState()
    {
        if (InputExt.IsActionHolding(InputBindings.Shoot))
            Fsm.Set<PlayerStateShoot>();
    }

    protected void TrySetHoldObjectState()
    {
        if (Player.IsHoldingObject())
            Fsm.Set<PlayerStateHoldObject>();
    }
}