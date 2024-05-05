using Godot;
using Platformer.Scripts.Properties.Interfaces;

namespace Platformer.Scripts.Weapon;

public abstract partial class BaseBullet : CharacterBody2D, IShootable
{
    [Export] public int Damage { get; set; } = 1;
    [Export] public float Speed { get; set; } = 50;
    [Export] public int BulletCountAsOneShot { get; set; } = 1;

    public void Construct(Vector2 position, float rotation)
    {
        Position = position;
        Velocity = new Vector2(Speed, 0).Rotated(rotation);
    }

    public override void _PhysicsProcess(double delta)
    {
        KinematicCollision2D collision = MoveAndCollide(Velocity * (float)delta);
        ProcessCollision(collision);
    }

    protected abstract void ProcessCollision(KinematicCollision2D? collision);

}