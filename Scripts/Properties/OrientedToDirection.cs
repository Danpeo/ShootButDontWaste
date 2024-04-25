using Godot;

namespace Platformer.Scripts.Properties;

public partial class OrientedToDirection : Node
{
    private CharacterBody2D _body = null!;
    private bool _flipOrientation;

    private bool FlipOrientation
    {
        get => _flipOrientation;
        set
        {
            if (_flipOrientation != value)
            {
                _body.Scale = new Vector2(-1, _body.Scale.Y);
                _flipOrientation = value;
            }
        }
    }

    public override void _PhysicsProcess(double delta) => 
        DefineOrientation(_body.Velocity);

    public override void _Ready() => 
        _body = GetParent<CharacterBody2D>();

    private void DefineOrientation(Vector2 velocity)
    {
        FlipOrientation = velocity.X switch
        {
            > 0 => false,
            < 0 => true,
            _ => FlipOrientation
        };
    }
}