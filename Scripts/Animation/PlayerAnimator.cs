using Godot;
using Platformer.Scripts.Effects;
using Platformer.Scripts.Entities;
using Platformer.Scripts.Utils;

namespace Platformer.Scripts.Animation;

public partial class PlayerAnimator : AnimatedSprite2D
{
    private Player _player;
    private bool _hit;
    private Timer _hitTimer;

    public override void _Ready()
    {
        _hitTimer = GetNode<Timer>("HitTimer");
        _hitTimer.Timeout += () => _hit = false;

        _player = GetParent<Player>();
        _player.Ready += () =>
        {
            _player.Ammo.OnReducedByDamage += () =>
            {
                _hit = true;
                _hitTimer.Start();
                this.Autoload<FrameFreeze>("FrameFreeze").Activate(0.05f, (float)_hitTimer.WaitTime);
            };
        };

        PlayIdle();
    }

    public override void _Process(double delta)
    {
        PlayAnimation();
    }

    private void PlayAnimation()
    {
        if (_hit)
        {
            Play("Hit");
            return;
        }

        if (_player.Velocity.X is > 0 or < 0)
        {
            Play("Run");
        }
        else if (_player.Velocity.Y < 0)
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