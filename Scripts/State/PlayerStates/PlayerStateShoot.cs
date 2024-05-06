using DVar.ShootButDontWaste.Animations.AnimationTypes;
using DVar.ShootButDontWaste.Constants;
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
        Player.PlayAnimation(APlayer.Shoot);
    }

    public override void PhysicsProcess(double delta)
    {
        if (!InputExt.IsActionHolding(InputBindings.shoot) || Player.IsHoldingObject())
        {
            Fsm.Set<PlayerStateIdle>();
            return;
        }
        
        Player.Shoot();
    }
}