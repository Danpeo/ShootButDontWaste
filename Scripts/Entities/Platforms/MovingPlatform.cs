using Godot;

namespace Platformer.Scripts.Entities.Platforms;

public partial class MovingPlatform : Node2D
{
    [Export] private float _duration = 5f;
    [Export] private Vector2[] _offsets = null!;

    public override void _Ready()
    {
        var tween = GetTree().CreateTween().SetProcessMode(Tween.TweenProcessMode.Physics);
        tween.SetLoops().SetParallel(false);
        var animatableBody2d = GetNode<AnimatableBody2D>("AnimatableBody2D");

        foreach (Vector2 offset in _offsets)
        {
            tween.TweenProperty(animatableBody2d, "position", offset, _duration / _offsets.Length);
        }
    }
}