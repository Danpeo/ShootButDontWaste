using Godot;
using Platformer.Scripts.Entities;

namespace Platformer.Scripts.Properties;

public partial class ParentCanDamageOnTouch : Node
{
    [Export] public int Damage { get; set; } = 1;
    [Export] private float _coolDown = 0.75f;
    private Area2D _area = null!;
    private Player? _player;
    private Timer _damageTimer = null!;
    private bool _canDamage = true;

    public override void _Ready()
    {
        _area = GetParent<Area2D>();
        _damageTimer = GetNode<Timer>("DamageTimer");
        _damageTimer.WaitTime = _coolDown;
        _damageTimer.Timeout += () => _canDamage = true;
        _area.Ready += () =>
        {
            _area.BodyEntered += body =>
            {
                if (body is Player player)
                {
                    _player = player;
                    AffectPlayer.Damage(player, Damage);
                    _canDamage = false;
                    _damageTimer.Start();
                }
            };

            _area.BodyExited += body =>
            {
                if (body is Player)
                {
                    _player = null;
                }
            };
        };
    }

    public override void _PhysicsProcess(double delta)
    {
        if (_canDamage && _player != null && _area.OverlapsBody(_player))
        {
            AffectPlayer.Damage(_player, Damage);
            _canDamage = false;
            _damageTimer.Start();
        }
    }
}