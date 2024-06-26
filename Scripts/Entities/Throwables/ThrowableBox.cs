using Godot;
using Platformer.Scripts.Constants.Sounds;
using Platformer.Scripts.Entities.Enemies;
using Platformer.Scripts.Properties;
using Platformer.Scripts.Properties.Interfaces;
using Platformer.Scripts.State;
using Platformer.Scripts.State.ThrowableStates;
using Platformer.Scripts.Utils;

namespace Platformer.Scripts.Entities.Throwables;

public partial class ThrowableBox : CharacterBody2D, IThrowable
{
    [Export] private float _xVelocity = 200f;
    [Export] private float _yVelocity = -80f;
    [Export] private float _holdingOffset = 5f;
    [Export] private int _damage = 2;
    public bool IsHeld { get; private set; }
    private InteractArea _pickableArea = null!;
    private Vector2 _originalPosition;
    private float _direction;
    private Fsm _fsm = null!;
    private AudioStreamPlayer2D _audioPlayer = null!;

    public override void _Ready()
    {
        _originalPosition = Position;
        _pickableArea = GetNode<InteractArea>("InteractArea");
        _fsm = new Fsm();
        _audioPlayer = GetNode<AudioStreamPlayer2D>("AudioStreamPlayer2D");
        _fsm.Add(new ThrowableStateIdle(_fsm, this));
        _fsm.Add(new ThrowableStatePickup(_fsm, this));
        _fsm.Add(new ThrowableStateThrow(_fsm, this));
        _fsm.Add(new ThrowableStateDrop(_fsm, this));
        _fsm.Set<ThrowableStateIdle>();
    }

    public override void _PhysicsProcess(double delta)
    {
        Vector2 currVelocity = Velocity;
        currVelocity.Y += World.GetGravity() * (float)delta;
        Velocity = currVelocity;

        var collision = MoveAndCollide(Velocity * (float)delta);
        if (collision?.GetCollider() is IEnemy enemy)
        {
            _audioPlayer.PlayAudio(CommonSounds.BlockHit);
            EnemyAffect.Damage(enemy, _damage);
            Velocity = Velocity.Bounce(collision.GetNormal());
        }

        if (IsOnFloor())
        {
            Velocity = Vector2.Zero;
        }

        if (IsHeld && IsPlayerInArea())
        {
            Player player = _pickableArea.I.Player!;
            Position = player.Position +
                       player.Transform.X * _holdingOffset;
            Velocity = Vector2.Zero;
            _direction = player.Scale.Y;
            player.CurrentThrowableObject = this;
        }

        _fsm.PhysicsProcess(delta);
    }

    public void Pickup()
    {
        IsHeld = true;
    }

    public void Drop()
    {
        IsHeld = false;
    }

    public void Throw()
    {
        if (IsHeld && IsPlayerInArea())
        {
            IsHeld = false;
            Velocity = Velocity with { X = _xVelocity * _direction, Y = _yVelocity };
            _pickableArea.I.Player!.CurrentThrowableObject = null;
        }
    }

    public bool IsPlayerInArea() =>
        _pickableArea.I.Player != null;

    private void OnVisibilityNotifier2DScreenExited() => QueueFree();
}