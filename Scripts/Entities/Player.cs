using System;
using System.Diagnostics;
using Godot;
using Platformer.Scripts.Animations;
using Platformer.Scripts.Constants;
using Platformer.Scripts.Effects;
using Platformer.Scripts.Properties;
using Platformer.Scripts.Properties.Interfaces;
using Platformer.Scripts.State;
using Platformer.Scripts.State.PlayerStates;
using Platformer.Scripts.Utils;

namespace Platformer.Scripts.Entities;

public partial class Player : CharacterBody2D
{
    [Export] public float Speed { get; set; } = 250f;
    [Export] public float JumpSpeed { get; set; } = -300f;
    [Export] private float _stunDistanceX = 40f;
    [Export] private float _stunDistanceY = -180f;
    public Ammo Ammo { get; private set; } = null!;

    public IThrowable? CurrentThrowableObject { get; set; }

    private AnimatedSprite2D _playerAnimator = null!;
    private AnimatedSprite2D _playerAnimatorUnarmed = null!;

    private SpriteFrames _armedSpriteFrames =
        GD.Load<SpriteFrames>("res://Scenes/Animators/ArmedPlayerSpriteFrames.tres");

    private SpriteFrames _unarmedSpriteFrames =
        GD.Load<SpriteFrames>("res://Scenes/Animators/UnarmedPlayerSpriteFrames.tres");

    public float Direction { get; internal set; }
    private CanShoot _canShoot = null!;
    private OrientedToDirection _orientedToDirection = null!;
    private Fsm _fsm = null!;

    public override void _Ready()
    {
        _orientedToDirection = GetNode<OrientedToDirection>("OrientedToDirection");

        _playerAnimator = GetNode<AnimatedSprite2D>("%PlayerAnimatedSprite");
        //_playerAnimatorUnarmed = GetNode<AnimatedSprite2D>("PlayerAnimatedSpriteUnarmed");
        Ammo = GetNode<Ammo>("%PlayerAmmo");
        Ammo.OnAmmoLessThanZero += die;
        _canShoot = GetNode<CanShoot>("%PlayerCanShoot");
        _canShoot.OnShooted += shots => Ammo.ReduceByShooting(shots);
        AddStates();

        void die()
        {
            GetTree().ReloadCurrentScene();
        }
    }

    public override void _PhysicsProcess(double delta)
    {
        /*
        Debug.Print(Ammo.Current + "AMMO");
        */
        Vector2 currVelocity = Velocity;
        currVelocity.Y += World.GetGravity() * (float)delta;
        currVelocity.X = Direction * Speed;
        Velocity = currVelocity;

        MoveAndSlide();
        CheckCollisions();

        _fsm.PhysicsProcess(delta);
    }

    public void OnAmmoReducedByDamage(Action action) =>
        Ammo.OnReducedByDamage += action;

    public bool IsHoldingObject() =>
        CurrentThrowableObject != null;

    public void Move() =>
        Direction = Input.GetAxis(InputBindings.MoveLeft, InputBindings.MoveRight);

    public void Jump() =>
        Velocity = Velocity with { Y = JumpSpeed };

    public void Shoot() =>
        _canShoot.Shoot(Rotation, Ammo.Current);

    public void PlayAnimation(PlayerAnim animation)
    {
        SetSpriteFrames();
        _playerAnimator.Play(PlayerAnimation.Value(animation));
    }

    public void Hit(float frameFreezeDuration)
    {
        PlayAnimation(PlayerAnim.Hit);
        const float frameFreezeDurationMultiplier = 1.5f;
        const float frameFreezeTiemScale = 0.05f;
        this.Autoload<FrameFreeze>("FrameFreeze")
            .Activate(frameFreezeTiemScale, frameFreezeDuration * frameFreezeDurationMultiplier);

        bool flippedDirection = _orientedToDirection.FlipOrientation;

        Direction = flippedDirection switch
        {
            true when Direction == 0 => 1,
            false when Direction == 0 => -1,
            _ => -Direction
        };

        Velocity = new Vector2(Direction * _stunDistanceX, _stunDistanceY);
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

    private void CheckCollisions()
    {
        for (int i = 0; i < GetSlideCollisionCount(); i++)
        {
            KinematicCollision2D collision = GetSlideCollision(i);
            const float error = 0.1f;
            if (collision.GetCollider() is ISquashable squashable)
            {
                // We check that we are hitting it from above.
                if (Vector2.Up.Dot(collision.GetNormal()) > error)
                {
                    squashable.GetSquashed();
                }
            }
        }
    }

    private void SetSpriteFrames()
    {
        _playerAnimator.SpriteFrames = IsHoldingObject() ? _unarmedSpriteFrames : _armedSpriteFrames;
    }
}