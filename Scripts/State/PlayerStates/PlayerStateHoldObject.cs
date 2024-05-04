using Platformer.Scripts.Entities;

namespace Platformer.Scripts.State.PlayerStates;

public class PlayerStateHoldObject : PlayerStateIdle
{
    public PlayerStateHoldObject(Fsm fsm, Player player) : base(fsm, player)
    {
    }

    public override void Enter()
    {
    }

    public override void PhysicsProcess(double delta)
    {
        base.PhysicsProcess(delta);
    }

    /*
    public override void Exit()
    {
        Player.SetSpriteFrames();
    }
*/
}