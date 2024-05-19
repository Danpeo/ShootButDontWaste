using Godot;
using Platformer.Scripts.Entities.Enemies;
using Platformer.Scripts.Properties;

namespace Platformer.Scripts.Weapon;

public partial class PlayerBullet : BaseBullet
{
    protected override void ProcessCollision(KinematicCollision2D? collision)
    {
        if (collision != null)
        {
            Collided = true;
            Destroy();
            if (collision.GetCollider() is IEnemy enemy)
            {
                EnemyAffect.Damage(enemy, Damage);
            }
        }
    }

    private void OnVisibilityNotifier2DScreenExited() => Destroy();
}