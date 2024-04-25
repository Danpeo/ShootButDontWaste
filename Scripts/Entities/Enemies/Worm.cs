using Godot;
using Platformer.Scripts.Properties;

namespace Platformer.Scripts.Entities.Enemies;

public partial class Worm : CharacterBody2D, IEnemy
{
    public Health Health { get; set; } = null!;
    private OrientedToDirection _orientedToDirection;
    
    private Vector2 startingPosition;
    private Vector2 targetPosition;
    private bool movingToTarget = true;
    private float speed = 20f;

    public override void _Ready()
    {
        Health = GetNode<Health>("%WormHealth");
        Health.OnHealthIsZero += QueueFree;

        _orientedToDirection = GetNode<OrientedToDirection>("OrientedToDirection");
        
        startingPosition = Position;
        targetPosition = new Vector2(startingPosition.X + 50, startingPosition.Y);
    }

    public override void _Process(double delta)
    {
        GD.Print($"WORM HEALTH: {Health.Current}");
        if (movingToTarget)
        {
            MoveToTarget(targetPosition);
        }
        else
        {
            MoveToTarget(startingPosition);
        }
    }

    private void MoveToTarget(Vector2 target)
    {
        Vector2 directionToTarget = (target - Position).Normalized();
        Velocity = directionToTarget * speed;

        MoveAndSlide();
        if (Position.DistanceTo(target) < 10)
        {
            movingToTarget = !movingToTarget;
        }
    }
}