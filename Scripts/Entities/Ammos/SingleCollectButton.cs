using Platformer.Scripts.Utils;
namespace Platformer.Scripts.Entities.Ammos;

public partial class SingleCollectButton : CollectButtonBase
{
    public override void _Ready()
    {
        this.PlayDefaultAnimation();
        
        BodyEntered += body =>
        {
            OnCollect(body);
            QueueFree();
        };
    }
}