namespace Platformer.Scripts.State;

public abstract class FsmState
{
    protected readonly Fsm Fsm;

    protected FsmState(Fsm fsm)
    {
        Fsm = fsm;
    }

    public virtual void Enter()
    {
    }

    public virtual void Exit()
    {
    }

    public virtual void PhysicsProcess(double delta)
    {
    }

    public virtual void Process(double delta)
    {
        
    }
}