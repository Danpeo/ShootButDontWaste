using Godot;
using Platformer.Scripts.Properties;

namespace Platformer.Scripts.Entities.Enemies;

public partial class Mushroom : CharacterBody2D, IEnemy
{
    [Export] private bool _flipped = true;
    public Health Health { get; set; } = null!;
    private CanShoot _canShoot = null!;

    public override void _Ready()
    {
        Health = GetNode<Health>("Health");
        _canShoot = GetNode<CanShoot>("CanShoot");
    }

    public override void _Draw()
    {
        if (_flipped)
        {
            Scale = new Vector2(-Scale.X, Scale.Y);
        }
    }

    public override void _PhysicsProcess(double delta)
    {
        _canShoot.Shoot(Scale.X, int.MaxValue);
    }
}