using System.Diagnostics;
using Godot;
using Platformer.Scripts.Animation;
using Platformer.Scripts.Properties;
using Platformer.Scripts.Utils;

namespace Platformer.Scripts.Entities;

public partial class Player : CharacterBody2D
{
    [Export] public float Speed { get; set; } = 250;
    [Export] public float JumpSpeed { get; set; } = -300;
    public Health Health { get; set; }
    private float _gravity = ProjectSettings.GetSetting(SettingConstant.Gravity).AsSingle();
    private PlayerAnimator _animatedSprite;
    private CanShoot _canShoot;
    private bool _flipOrientation;

    private bool FlipOrientation
    {
        get => _flipOrientation;
        set
        {
            if (_flipOrientation != value)
            {
                Scale = new Vector2(-1, Scale.Y);
                _flipOrientation = value;
            }
        }
    }

    public override void _Ready()
    {
        Health = GetNode<Health>("%Health");
        _animatedSprite = GetNode<PlayerAnimator>("%PlayerAnimatedSprite");
        _canShoot = GetNode<CanShoot>("%PlayerCanShoot");
    }

    public override void _PhysicsProcess(double delta)
    {
        Debug.Print(Health.Current + "HEALTH");
        Vector2 currVelocity = Velocity;
        currVelocity.Y += _gravity * (float)delta;

        if (Input.IsActionJustPressed("Jump") && IsOnFloor())
            currVelocity.Y = JumpSpeed;

        float direction = Input.GetAxis("MoveLeft", "MoveRight");
        currVelocity.X = direction * Speed;

        _animatedSprite.PlayAnimation(currVelocity);

        DefineOrientation(currVelocity);
        Velocity = currVelocity;
        MoveAndSlide();

        if (InputExt.IsActionHolding("Shoot"))
        {
            _canShoot.Shoot(Rotation);
        }
    }

    private void DefineOrientation(Vector2 velocity)
    {
        if (velocity.X > 0)
        {
            FlipOrientation = false;
        }
        else if (velocity.X < 0)
        {
            FlipOrientation = true;
        }
    }
}