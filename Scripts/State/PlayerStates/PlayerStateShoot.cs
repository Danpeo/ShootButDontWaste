using Platformer.Scripts.Constants;
using Platformer.Scripts.Constants.Animations;
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
        Player.PlayAnimation(PlayerAnimation.Shoot);
    }

    public override void PhysicsProcess(double delta)
    {
        if (!InputExt.IsActionHolding(InputBindings.Shoot))
        {
            Fsm.Set<PlayerStateIdle>();
            return;
        }
        
        Player.Shoot();
    }
}