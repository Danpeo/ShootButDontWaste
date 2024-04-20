using Godot;
using Platformer.Scripts.Properties;

namespace Platformer.Scripts.Entities;

public partial class DeathFloor : Area2D
{
    public override void _Ready()
    {
        BodyEntered += body =>
        {
            if (body is Player player)
            {
                Health playerHealth = player.Health;
                playerHealth.Current = 0;
            }
        };
    }
}