using System;
using Godot;
using Platformer.Scripts.Animations;
using Platformer.Scripts.Constants;
using Platformer.Scripts.Effects;
using Platformer.Scripts.Properties;
using Platformer.Scripts.Properties.Interfaces;
using Platformer.Scripts.Utils;

namespace Platformer.Scripts.Entities.Enemies;

public partial class Mushroom : CharacterBody2D, IEnemy, ISquashable
{
    [Export] private bool _flipped = true;
    public Health Health { get; set; } = null!;
    private CanShoot _canShoot = null!;
    private AnimatedSprite2D _animatedSprite = null!;
    
    public override void _Ready()
    {
        Health = GetNode<Health>("Health");
        _canShoot = GetNode<CanShoot>("CanShoot");
        _animatedSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
        _animatedSprite.Play("Shoot");

        _animatedSprite.AnimationFinished += () =>
        {
            if (_animatedSprite.Animation == MushroomAnimation.Value(MushroomAnim.Squash))
            {
                QueueFree();
            }
        };
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
        _canShoot.Shoot(Rotation, int.MaxValue);
    }

    public void Showup()
    {
        PlayAnimation(MushroomAnim.Showup);
    }

    public void PlayAnimation(MushroomAnim animation) =>
        _animatedSprite.Play(MushroomAnimation.Value(animation));

    public void GetSquashed()
    {
        GetNode<CollisionShape2D>(EntityConst.DamageAreaCollisionShape).Disabled = true;
        GetNode<CollisionShape2D>("CollisionShape2D").Disabled = true;
        
        const float frameFreezeDuration = 0.5f;
        const float frameFreezeTiemScale = 0.05f;
        this.Autoload<FrameFreeze>("FrameFreeze")
            .Activate(frameFreezeTiemScale, frameFreezeDuration);

        var random = new Random();
        bool randomBoolValue = random.Next(0, 2) == 0;
        int direction = randomBoolValue ? -1 : 1;
        Velocity = new Vector2(direction * 40f, -180f);

        PlayAnimation(MushroomAnim.Squash);
    }
}