using System;
using DVar.ShootButDontWaste.Animations;
using DVar.ShootButDontWaste.Animations.AnimationTypes;
using DVar.ShootButDontWaste.Constants;
using Godot;
using Platformer.Scripts.Constants;
using Platformer.Scripts.Constants.Sounds;
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

    private AudioStreamPlayer2D _sfxPlayer = null!;

    public float DirectionX { get; internal set; }
    public float DirectionY { get; internal set; }
    private CanShoot _canShoot = null!;
    private OrientedToDirection _orientedToDirection = null!;
    private Fsm _fsm = null!;

    public override void _Ready()
    {
        _orientedToDirection = GetNode<OrientedToDirection>("OrientedToDirection");
        _sfxPlayer = GetNode<AudioStreamPlayer2D>("SFXStreamPlayer2D");
        _playerAnimator = GetNode<AnimatedSprite2D>("%PlayerAnimatedSprite");
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

        currVelocity.X = DirectionX * Speed;
        Velocity = currVelocity;

        MoveAndSlide();
        CheckCollisions();

        _fsm.PhysicsProcess(delta);
    }

    public override void _Input(InputEvent @event)
    {
        goDown();

        void goDown()
        {
            if (InputExt.IsActionHolding(InputBindings.interact) && Input.IsActionPressed(InputBindings.down) &&
                IsOnFloor())
            {
                Vector2 newPos = Position;
                newPos.Y += 1f;
                Position = newPos;
            }
        }
    }

    public void OnAmmoReducedByDamage(Action action) =>
        Ammo.OnReducedByDamage += action;

    public bool IsHoldingObject() =>
        CurrentThrowableObject != null;

    public void Move() =>
        DirectionX = Input.GetAxis(InputBindings.moveLeft, InputBindings.moveRight);

    public void Jump()
    {
        PlayAudio(PlayerSounds.Jump);
        
        Velocity = Velocity with { Y = JumpSpeed };
    }

    /*public void GoUp()
    {
        _isUsingLadder = true;
    }

    public void GoDown()
    {
        _isUsingLadder = false;
    }*/

    public void Shoot() => 
        _canShoot.Shoot(Rotation, Ammo.Current);

    public void PlayAnimation(APlayer animation)
    {
        SetSpriteFrames();
        _playerAnimator.Play(Anim.player(animation));
    }

    public void PlayAudio(AudioStream audioStream)
    {
        _sfxPlayer.Stream = audioStream;
        _sfxPlayer.Play();
    }

    public void Hit(float frameFreezeDuration)
    {
        PlayAudio(PlayerSounds.Hit);
        PlayAnimation(APlayer.Hit);
        const float frameFreezeDurationMultiplier = 1.5f;
        const float frameFreezeTiemScale = 0.05f;
        this.Autoload<FrameFreeze>("FrameFreeze")
            .Activate(frameFreezeTiemScale, frameFreezeDuration * frameFreezeDurationMultiplier);

        bool flippedDirection = _orientedToDirection.FlipOrientation;

        DirectionX = flippedDirection switch
        {
            true when DirectionX == 0 => 1,
            false when DirectionX == 0 => -1,
            _ => -DirectionX
        };

        Velocity = new Vector2(DirectionX * _stunDistanceX, _stunDistanceY);
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