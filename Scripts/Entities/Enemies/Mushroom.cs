using System;
using DVar.ShootButDontWaste.Animations;
using DVar.ShootButDontWaste.Animations.AnimationTypes;
using Godot;
using Platformer.Scripts.Constants;
using Platformer.Scripts.Constants.Sounds;
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
    private AudioStreamPlayer2D _audioPlayer = null!;

    public override void _Ready()
    {
        Health = GetNode<Health>("Health");
        Health.OnHealthIsZero += GetSquashed;
        _audioPlayer = GetNode<AudioStreamPlayer2D>("AudioStreamPlayer2D");
        
        _canShoot = GetNode<CanShoot>("CanShoot");
        _spotArea = GetNode<SpotArea>("SpotArea");

        _spotArea.OnSpotted += Showup;

        _spotArea.OnLosed += Idle;

        _animatedSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
        _animatedSprite.Play("Shoot");

        _animatedSprite.AnimationFinished += () =>
        {
            if (_animatedSprite.Animation == Anim.mushroom(AMushroom.Squash))
            {
                QueueFree();
            }
            else if (_animatedSprite.Animation == Anim.mushroom(AMushroom.Showup))
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
        PlayAnimation(AMushroom.Showup);
    }

    private void Shoot()
    {
        PlayAnimation(AMushroom.Shoot);
        _canShoot.Shoot(Rotation, int.MaxValue);
    }

    public void Idle()
    {
        if (_animatedSprite.Animation == Anim.mushroom(AMushroom.Shoot) ||
            _animatedSprite.Animation == Anim.mushroom(AMushroom.Showup))
        {
            PlayAnimation(AMushroom.Hide);
        }

        PlayAnimation(AMushroom.Idle);
        _shoot = false;
    }

    /*private void PlayAnimation(MushroomAnim animation) =>
        _animatedSprite.Play(MushroomAnimation.Value(animation));*/

    private void PlayAnimation(AMushroom animation) =>
        _animatedSprite.Play(Anim.mushroom(animation));

    public void Hit()
    {
        PlayAnimation(AMushroom.Hit);
        FrameFreeze();
        this.Stun();
    }

    public void GetSquashed()
    {
        _audioPlayer.PlayAudio(CommonSounds.Bubble);
        SetCollisionDisabled(true);

        FrameFreeze();

        this.Stun();

        PlayAnimation(AMushroom.Squash);
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