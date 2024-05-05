using Godot;
using Platformer.Scripts.Entities;
using Platformer.Scripts.Properties;

namespace Platformer.Scripts.Weapon;

public partial class MushroomBullet : BaseBullet
{
    protected override void ProcessCollision(KinematicCollision2D? collision)
    {
        if (collision != null)
        {
            QueueFree();

            if (collision.GetCollider() is Player player)
            {
                PlayerAffect.Damage(player, Damage);
            }
        }
    }

    private void OnVisibilityNotifier2DScreenExited() => QueueFree();
}