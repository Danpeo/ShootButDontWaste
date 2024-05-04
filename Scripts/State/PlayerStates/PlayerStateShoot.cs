using Platformer.Scripts.Animations;
using Platformer.Scripts.Constants;
using Platformer.Scripts.Entities;
using Platformer.Scripts.Utils;

namespace Platformer.Scripts.State.PlayerStates;

public class PlayerStateShoot : PlayerState
{
    public PlayerStateShoot(Fsm fsm, Player player) : base(fsm, player)
    {
    }

    public override void Enter()
    {
        Player.PlayAnimation(PlayerAnim.Shoot);
    }

    public override void PhysicsProcess(double delta)
    {
        if (!InputExt.IsActionHolding(InputBindings.Shoot) || Player.IsHoldingObject())
        {
            Fsm.Set<PlayerStateIdle>();
            return;
        }
        
        Player.Shoot();
    }
}