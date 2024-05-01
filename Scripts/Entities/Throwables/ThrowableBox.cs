using Godot;
using Platformer.Scripts.Properties;

namespace Platformer.Scripts.Entities.Throwables;

public partial class ThrowableBox : CharacterBody2D
{
    [Export] private float _speed = 500f;
    private bool _isHeld;
    private InteractArea _pickableArea = null!;
    private Vector2 _originalPosition;

    public override void _Ready()
    {
        _originalPosition = Position;
        _pickableArea = GetNode<InteractArea>("%PickableArea");
    }

    public override void _PhysicsProcess(double delta)
    {
        /*if (IsPlayerInArea() && Input.IsActionPressed("Pickup"))
        {
            Pickup();
        }

        if (_isHeld && IsPlayerInArea())
        {
            Player player = _pickableArea.I.Player!;
            Position = player.Position +
                       player.Transform.X * 5f;
        }

        if (_isHeld && Input.IsActionPressed("Throw"))
        {
            Throw((float)delta);
        }*/

        Velocity = Velocity with { Y = World.GetGravity() * (float)delta, X = 30f };
    }

    public void Pickup()
    {
        if (IsPlayerInArea() && !_isHeld)
        {
            _isHeld = true;
        }
    }

    public void Throw(float delta)
    {
        if (_isHeld && IsPlayerInArea())
        {
            _isHeld = false;

            Velocity = new Vector2(_speed, 50).Rotated(Rotation);

            Vector2 throwDirection = (Position - _pickableArea.I.Player!.Position).Normalized();
            KinematicCollision2D collision = MoveAndCollide(Velocity * delta);
        }
    }

    private bool IsPlayerInArea() =>
        _pickableArea.I.Player != null;

    private void OnVisibilityNotifier2DScreenExited() => QueueFree();
}