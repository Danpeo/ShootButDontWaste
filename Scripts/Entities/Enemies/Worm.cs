using System;
using System.Diagnostics;
using Godot;
using Platformer.Scripts.Animations;
using Platformer.Scripts.Effects;
using Platformer.Scripts.Entities.Areas;
using Platformer.Scripts.Properties;
using Platformer.Scripts.State;
using Platformer.Scripts.State.WormStates;

namespace Platformer.Scripts.Entities.Enemies;

public partial class Worm : CharacterBody2D, IEnemy
{
    [Export(PropertyHint.Range, "0,500,")] public float OriginalSpeed { get; set; } = 20f;
    [Export(PropertyHint.Range, "0,500,")] public float IncreasedSpeed { get; set; } = 30f;

    [Export(PropertyHint.Range, "1,1000,")]
    public float PatrolDistance { get; set; } = 40f;

    [Export(PropertyHint.Range, "1,100,")] public float ChangeOfDirectionDistance { get; set; } = 1f;

    private float _currentSpeed;
    public Health Health { get; set; } = null!;
    public Exclamation Exclamation { get; private set; } = null!;

    private OrientedToDirection _orientedToDirection = null!;
    private SpotArea _spotArea = null!;
    private AnimatedSprite2D _animatedSprite = null!;
    private Vector2 _startingPosition;
    private Vector2 _targetPatrolPointPosition;
    private Vector2 _currentTargetPosition;
    private bool _movingToPatrolPoint = true;
    private Fsm _fsm = null!;

    public override void _Ready()
    {
        Health = GetNode<Health>("%WormHealth");
        Health.OnHealthIsZero += QueueFree;

        _orientedToDirection = GetNode<OrientedToDirection>("OrientedToDirection");
        _spotArea = GetNode<SpotArea>("SpotArea");
        _animatedSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
        _startingPosition = Position;
        _targetPatrolPointPosition = new Vector2(_startingPosition.X + PatrolDistance, _startingPosition.Y);
        Exclamation = GetNode<Exclamation>("Exclamation");
        _currentSpeed = OriginalSpeed;
        AddStates();
    }
    
    public override void _PhysicsProcess(double delta)
    {
        _fsm.PhysicsProcess(delta);
    }

    public void Patrol()
    {
        _currentTargetPosition = _movingToPatrolPoint ? _targetPatrolPointPosition : _startingPosition;
        PlayAnimation(WormAnim.Move);

        MoveToTarget(_currentTargetPosition);
    }

    public void PursuePlayer()
    {
        _currentTargetPosition = _spotArea.Player!.Position;
        PlayAnimation(WormAnim.AgroMove);
        MoveToTarget(_currentTargetPosition);
    }

    public bool IsPlayerInSpotArea() =>
        _spotArea.Player != null;

    public void PlayAnimation(WormAnim animation)
    {
        _animatedSprite.Play(WormAnimation.Value(animation));
    }

    private void MoveToTarget(Vector2 target)
    {
        Vector2 directionToTarget = (target - Position).Normalized();
        Velocity = directionToTarget * _currentSpeed;

        MoveAndSlide();
    }

    public void Stop()
    {
        Velocity = Vector2.Zero;
        PlayAnimation(WormAnim.Idle);
    }

    public void OnSpottedPlayer(Action action)
    {
        _spotArea.OnSpotted += () =>
        {
            _currentSpeed = IncreasedSpeed;
            action.Invoke();
        };
    }

    public void OnLosedPlayer(Action action)
    {
        _spotArea.OnLosed += () =>
        {
            _currentSpeed = OriginalSpeed;
            action.Invoke();
        };
    }

    public bool IsAlmostAtCurrentTargetPosition() =>
        Position.DistanceTo(_currentTargetPosition) < ChangeOfDirectionDistance;

    public void SwapTargetToOpposite() =>
        _movingToPatrolPoint = !_movingToPatrolPoint;

    private void AddStates()
    {
        _fsm = new Fsm();
        _fsm.Add(new WormStatePatrol(_fsm, this));
        _fsm.Add(new WormStateIdle(_fsm, this));
        _fsm.Add(new WormStatePrePursue(_fsm, this));
        _fsm.Add(new WormStatePursue(_fsm, this));
        _fsm.Set<WormStatePatrol>();
    }
}