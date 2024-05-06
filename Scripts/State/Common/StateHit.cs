using Godot;
using Platformer.Scripts.Properties.Interfaces;
using Platformer.Scripts.Utils;

namespace Platformer.Scripts.State.Common;

public class StateHit<TEntity, TSetStateAfterTimeOut> : FsmState
    where TEntity : Node, IHittableEnemy where TSetStateAfterTimeOut : FsmState
{
    private readonly EasyTimer _hitTimer;
    private readonly TEntity _entity;

    public StateHit(Fsm fsm, TEntity entity, float waitTime) : base(fsm)
    {
        _entity = entity;
        _hitTimer = new EasyTimer(_entity, () => Fsm.Set<TSetStateAfterTimeOut>(), waitTime);
    }
    public override void Enter()
    {
        _hitTimer.Start();
        _entity.Hit();
    }

    public override void Exit()
    {
        _hitTimer.Stop();
    }

}