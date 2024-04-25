using Godot;
using Platformer.Scripts.Properties;

namespace Platformer.Scripts.Entities.Enemies;

public partial class Worm : CharacterBody2D, IEnemy
{
    public Health Health { get; set; } = null!;

    public override void _Ready()
    {
        Health = GetNode<Health>("%WormHealth");
        Health.OnHealthIsZero += QueueFree;
    }

    public override void _Process(double delta)
    {
        GD.Print($"WORM HEALTH: {Health.Current}");
    }
}