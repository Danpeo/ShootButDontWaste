using Godot;

namespace Platformer.Scripts.Animation;

public partial class PlayerAnimator : AnimatedSprite2D
{
    public override void _Ready()
    {
        PlayIdle();
    }

    public void PlayAnimation(Vector2 velocity)
    {
        if (velocity.X is > 0 or < 0)
        {
            Play("Run");
        }
        else if (velocity.Y < 0)
        {
            Play("Jump");
        }
        else
        {
            PlayIdle();
        }
    }

    private void PlayIdle() => Play("Idle");
}