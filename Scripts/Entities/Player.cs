using System.Diagnostics;
using Godot;
using Platformer.Scripts.Properties;

namespace Platformer.Scripts.Entities;

public partial class Player : CharacterBody2D
{
    [Export] public float Speed { get; set; } = 250;
    [Export] public float JumpSpeed { get; set; } = -300;
    public float Direction { get; set; }
    public bool Jumped { get; set; }
    public Ammo Ammo { get; private set; }
    private float _gravity = ProjectSettings.GetSetting(SettingConstant.Gravity).AsSingle();
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
        Ammo = GetNode<Ammo>("%PlayerAmmo");
        Ammo.OnAmmoLessThanZero += die;
        _canShoot = GetNode<CanShoot>("%PlayerCanShoot");
        _canShoot.OnShooted += shots => Ammo.ReduceByShooting(shots);
        return;

        void die()
        {
            GetTree().ReloadCurrentScene();
        }
    }

    public override void _PhysicsProcess(double delta)
    {
        Debug.Print(Ammo.Current + "AMMO");
        Vector2 currVelocity = Velocity;
        currVelocity.Y += _gravity * (float)delta;

        if (Jumped)
        {
            currVelocity.Y = JumpSpeed;
            Jumped = false;
        }

        currVelocity.X = Direction * Speed;

        DefineOrientation(currVelocity);
        Velocity = currVelocity;
        MoveAndSlide();
    }

    public void Shoot() => _canShoot.Shoot(Rotation, Ammo.Current);

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