using Godot;
using Platformer.Scripts.Properties;

namespace Platformer.Scripts.Entities.Ammos;

public abstract partial class CollectButtonBase : Area2D
{
    [Export] public int CollectValue { get; set; } = 1;

    protected void OnCollect(Node2D body)
    {
        if (body is Player player)
        {
            Ammo playerAmmo = player.Ammo;
            playerAmmo.Add(CollectValue);
        }
    }
}