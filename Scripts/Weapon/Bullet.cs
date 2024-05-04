using Godot;
using Platformer.Scripts.Entities.Enemies;
using Platformer.Scripts.Properties;
using Platformer.Scripts.Properties.Interfaces;

namespace Platformer.Scripts.Weapon;

public partial class Bullet : CharacterBody2D, IShootable
{
    [Export] public int Damage { get; set; } = 1;
    [Export] public float Speed { get; set; } = 50;
    [Export] public int BulletCountAsOneShot { get; set; } = 1;

    public void Construct(Vector2 position, float direction)
    {
        Position = position;
        Velocity = new Vector2(Speed, 0).Rotated(direction);
    }

    public override void _PhysicsProcess(double delta)
    {
        KinematicCollision2D collision = MoveAndCollide(Velocity * (float)delta);
        if (collision != null)
        {
            if (collision.GetCollider() is IEnemy enemy)
            {
                Health enemyHealth = enemy.Health;
                enemyHealth.Reduce(Damage);
                QueueFree();
            }
        }
    }

    private void OnVisibilityNotifier2DScreenExited() => QueueFree();
}