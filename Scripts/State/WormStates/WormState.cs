using System;
using Platformer.Scripts.Constants.Times;
using Platformer.Scripts.Entities.Enemies;
using Platformer.Scripts.Utils;

namespace Platformer.Scripts.State.WormStates;

public class WormState : FsmState
{
    protected readonly Worm Worm;
    private EasyTimer _idleTimer = null!;

    protected WormState(Fsm fsm, Worm worm) : base(fsm)
    {
        Worm = worm;
        Worm.Ready += () => Worm.OnSpottedPlayer(() => Fsm.Set<WormStatePrePursue>());
        Worm.Ready += () => Worm.OnLosedPlayer(() => Fsm.Set<WormStatePatrol>());
        Worm.Ready += () => Worm.OnHeatlhDamaged(() => Fsm.Set<WormStateHit>());
    }

    protected void Stop(Action onTimeOut, float waitTime = WormTime.Idle)
    {
        _idleTimer = new EasyTimer(Worm, onTimeOut, waitTime)
        {
            QueueFreeOnTimeout = true
        };
        _idleTimer.Start();
        Worm.Stop();
    }
}