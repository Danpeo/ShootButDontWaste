using Godot;
using Platformer.Scripts.Properties;

namespace Platformer.Scripts.Entities.Ammos;

public abstract partial class CollectBulletBase : Area2D
{
    [Export] public int CollectValue { get; set; } = 1;

    protected bool TryCollect(Node2D body)
    {
        if (body is Player player)
        {
            Ammo playerAmmo = player.Ammo;
            playerAmmo.Add(CollectValue);
            return true;
        }

        return false;
    }
}