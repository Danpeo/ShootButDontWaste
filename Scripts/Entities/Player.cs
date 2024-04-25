using System.Diagnostics;
using Godot;
using Platformer.Scripts.Constants;
using Platformer.Scripts.Properties;
using Platformer.Scripts.State;
using Platformer.Scripts.State.PlayerStates;

namespace Platformer.Scripts.Entities;

public partial class Player : CharacterBody2D
{
    [Export] public float Speed { get; set; } = 250;
    [Export] public float JumpSpeed { get; set; } = -300;
    public AnimatedSprite2D PlayerAnimator { get; set; }
    public float Direction { get; set; }
    public Ammo Ammo { get; private set; } = null!;
    private float _gravity = ProjectSettings.GetSetting(SettingConstant.Gravity).AsSingle();
    private CanShoot _canShoot = null!;
    private bool _flipOrientation;
    private Fsm _fsm;


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
        PlayerAnimator = GetNode<AnimatedSprite2D>("%PlayerAnimatedSprite");
        Ammo = GetNode<Ammo>("%PlayerAmmo");
        Ammo.OnAmmoLessThanZero += die;
        _canShoot = GetNode<CanShoot>("%PlayerCanShoot");
        _canShoot.OnShooted += shots => Ammo.ReduceByShooting(shots);

        AddStates();

        return;

        void die()
        {
            GetTree().ReloadCurrentScene();
        }
    }

    public override void _PhysicsProcess(double delta)
    {
        _fsm.Update(delta);

        Debug.Print(Ammo.Current + "AMMO");
        Vector2 currVelocity = Velocity;
        currVelocity.Y += _gravity * (float)delta;
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

    private void AddStates()
    {
        _fsm = new Fsm();
        _fsm.Add(new PlayerStateIdle(_fsm, this));
        _fsm.Add(new PlayerStateMove(_fsm, this));
        _fsm.Add(new PlayerStateJump(_fsm, this));
        _fsm.Add(new PlayerStateHit(_fsm, this));
        _fsm.Set<PlayerStateIdle>();
    }
}