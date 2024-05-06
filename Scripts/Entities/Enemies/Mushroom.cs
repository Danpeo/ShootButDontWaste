using System;
using Godot;
using Platformer.Scripts.Animations;
using Platformer.Scripts.Constants;
using Platformer.Scripts.Effects;
using Platformer.Scripts.Entities.Areas;
using Platformer.Scripts.Properties;
using Platformer.Scripts.Properties.Interfaces;
using Platformer.Scripts.State;
using Platformer.Scripts.State.MushroomStates;
using Platformer.Scripts.Utils;

namespace Platformer.Scripts.Entities.Enemies;

public partial class Mushroom : CharacterBody2D, IEnemy, ISquashable, IHittableEnemy
{
    [Export] private bool _flipped = true;
    public Health Health { get; set; } = null!;
    private CanShoot _canShoot = null!;
    private SpotArea _spotArea = null!;
    private AnimatedSprite2D _animatedSprite = null!;
    private bool _isIdle;
    private bool _shoot;
    private Fsm _fsm = null!;

    public override void _Ready()
    {
        Health = GetNode<Health>("Health");
        Health.OnHealthIsZero += GetSquashed;

        _canShoot = GetNode<CanShoot>("CanShoot");
        _spotArea = GetNode<SpotArea>("SpotArea");

        _spotArea.OnSpotted += Showup;

        _spotArea.OnLosed += Idle;

        _animatedSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
        _animatedSprite.Play("Shoot");

        _animatedSprite.AnimationFinished += () =>
        {
            if (_animatedSprite.Animation == MushroomAnimation.Value(MushroomAnim.Squash))
            {
                QueueFree();
            }
            else if (_animatedSprite.Animation == MushroomAnimation.Value(MushroomAnim.Showup))
            {
                _shoot = true;
            }
        };

        AddStates();
    }

    public override void _Draw()
    {
        if (_flipped)
        {
            Scale = new Vector2(-Scale.X, Scale.Y);
        }
    }

    public override void _PhysicsProcess(double delta)
    {
        this.ApplyGravity(delta);
        MoveAndCollide(Velocity * (float)delta);

        _fsm.PhysicsProcess(delta);

        if (_shoot)
        {
            Shoot();
        }
    }

    public void OnSpottedPlayer(Action action) => _spotArea.OnSpotted += () => { action.Invoke(); };

    public void OnLosedPlayer(Action action) => _spotArea.OnLosed += () => { action.Invoke(); };

    public void OnHeatlhDamaged(Action action) =>
        Health.OnDamaged += action;

    public void Showup()
    {
        SetCollisionDisabled(false);
        PlayAnimation(MushroomAnim.Showup);
    }

    private void Shoot()
    {
        PlayAnimation(MushroomAnim.Shoot);
        _canShoot.Shoot(Rotation, int.MaxValue);
    }

    public void Idle()
    {
        if (_animatedSprite.Animation == MushroomAnimation.Value(MushroomAnim.Shoot) ||
            _animatedSprite.Animation == MushroomAnimation.Value(MushroomAnim.Showup))
        {
            PlayAnimation(MushroomAnim.Hide);
        }

        PlayAnimation(MushroomAnim.Idle);
        _shoot = false;
    }

    private void PlayAnimation(MushroomAnim animation) =>
        _animatedSprite.Play(MushroomAnimation.Value(animation));

    public void Hit()
    {
        SetCollisionDisabled(true);
        PlayAnimation(MushroomAnim.Hit);
        FrameFreeze();
        this.Stun();
    }

    public void GetSquashed()
    {
        SetCollisionDisabled(true);

        FrameFreeze();

        this.Stun();

        PlayAnimation(MushroomAnim.Squash);
    }

    private void FrameFreeze()
    {
        const float frameFreezeDuration = 0.5f;
        const float frameFreezeTiemScale = 0.05f;
        this.Autoload<FrameFreeze>("FrameFreeze")
            .Activate(frameFreezeTiemScale, frameFreezeDuration);
    }

    private void SetCollisionDisabled(bool disabled)
    {
        GetNode<CollisionShape2D>(EntityConst.DamageAreaCollisionShape).Disabled = disabled;
        GetNode<CollisionShape2D>("CollisionShape2D").Disabled = disabled;
    }

    private void AddStates()
    {
        _fsm = new Fsm();
        _fsm.Add(new MushroomStateIdle(_fsm, this));
        _fsm.Add(new MushroomStateSpotPlayer(_fsm, this));
        _fsm.Add(new MushroomStateLosePlayer(_fsm, this));
        _fsm.Add(new MushroomStateHit(_fsm, this));
        _fsm.Set<MushroomStateIdle>();
    }
}