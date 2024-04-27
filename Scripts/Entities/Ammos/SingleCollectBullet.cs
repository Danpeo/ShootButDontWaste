using Platformer.Scripts.Utils;

namespace Platformer.Scripts.Entities.Ammos;

public partial class SingleCollectBullet : CollectBulletBase
{
    public override void _Ready()
    {
        this.PlayDefaultAnimation();

        BodyEntered += body =>
        {
            if (TryCollect(body))
            {
                QueueFree();
            }
        };
    }
}