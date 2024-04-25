using System;
using System.Diagnostics;
using Godot;
using Platformer.Scripts.Constants;
using Platformer.Scripts.Constants.Animations;
using Platformer.Scripts.Effects;
using Platformer.Scripts.Properties;
using Platformer.Scripts.State;
using Platformer.Scripts.State.PlayerStates;
using Platformer.Scripts.Utils;

namespace Platformer.Scripts.Entities;

public partial class Player : CharacterBody2D
{
    [Export] public float Speed { get; set; } = 250;
    [Export] public float JumpSpeed { get; set; } = -300;
    private AnimatedSprite2D? _playerAnimator { get; set; }
    public float Direction { get; private set; }
    public Ammo Ammo { get; private set; } = null!;
    private float _gravity = ProjectSettings.GetSetting(SettingConstant.Gravity).AsSingle();
    private CanShoot _canShoot = null!;
    private OrientedToDirection _orientedToDirection = null!;
    private Fsm? _fsm;

    public override void _Ready()
    {
        _orientedToDirection = GetNode<OrientedToDirection>("OrientedToDirection");

        _playerAnimator = GetNode<AnimatedSprite2D>("%PlayerAnimatedSprite");
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
        _fsm!.Update(delta);

        Debug.Print(Ammo.Current + "AMMO");
        Vector2 currVelocity = Velocity;
        currVelocity.Y += _gravity * (float)delta;
        currVelocity.X = Direction * Speed;

        Velocity = currVelocity;
        MoveAndSlide();
    }

    public void OnAmmoReducedByDamage(Action action) =>
        Ammo.OnReducedByDamage += action;

    public void Move() =>
        Direction = Input.GetAxis(PlayerInput.MoveLeft, PlayerInput.MoveRight);

    public void Jump() =>
        Velocity = Velocity with { Y = JumpSpeed };

    public void Shoot() =>
        _canShoot.Shoot(Rotation, Ammo.Current);

    public void PlayAnimation(StringName animationName) =>
        _playerAnimator!.Play(animationName);

    public void Hit(float frameFreezeDuration)
    {
        Direction = 0;
        _playerAnimator!.Play(PlayerAnimation.Hit);
        const float frameFreezeDurationMultiplier = 1.5f;
        const float frameFreezeTiemScale = 0.05f;
        this.Autoload<FrameFreeze>("FrameFreeze")
            .Activate(frameFreezeTiemScale, frameFreezeDuration * frameFreezeDurationMultiplier);
    }

    private void AddStates()
    {
        _fsm = new Fsm();
        _fsm.Add(new PlayerStateIdle(_fsm, this));
        _fsm.Add(new PlayerStateMove(_fsm, this));
        _fsm.Add(new PlayerStateJump(_fsm, this));
        _fsm.Add(new PlayerStateHit(_fsm, this));
        _fsm.Add(new PlayerStateShoot(_fsm, this));
        _fsm.Set<PlayerStateIdle>();
    }
}