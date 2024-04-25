using Platformer.Scripts.Constants.Animations;
using Platformer.Scripts.Entities;
using Platformer.Scripts.Utils;

namespace Platformer.Scripts.State.PlayerStates;

public class PlayerState : FsmState
{
    protected readonly Player Player;

    public PlayerState(Fsm fsm, Player player) : base(fsm)
    {
        Player = player;
        Player.Ready += () => Player.Ammo.OnReducedByDamage += () => Fsm.Set<PlayerStateHit>();
    }

    public override void Enter()
    {
    }

    public override void Update(double delta)
    {

    }

    protected void TryPlayJump(string stateAnimation)
    {
        Player.PlayerAnimator.Play(Player.Velocity.Y < 0 ? PlayerAnimation.Jump : stateAnimation);
    }

    protected void TrySetJumpState()
    {
        if (InputExt.IsActionHolding(PlayerAnimation.Jump) && Player.IsOnFloor())
        {
            Fsm.Set<PlayerStateJump>();
        }
    }
}