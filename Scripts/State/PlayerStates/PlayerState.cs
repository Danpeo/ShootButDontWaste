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

    protected void TryPlayJump(string stateAnimation)
    {
        Player.PlayAnimation(Player.Velocity.Y < 0 ? PlayerInput.Jump : stateAnimation);
    }

    protected void TrySetJumpState()
    {
        if (InputExt.IsActionHolding(PlayerInput.Jump) && Player.IsOnFloor())
            Fsm.Set<PlayerStateJump>();
    }

    protected void TrySetShootState()
    {
        if (InputExt.IsActionHolding(PlayerInput.Shoot))
            Fsm.Set<PlayerStateShoot>();
    }
}