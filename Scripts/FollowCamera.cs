using Godot;

namespace Platformer.Scripts;

public partial class FollowCamera : Camera2D
{
    [Export] private float _randomStrength = 30f;
    [Export] private float _shakeFade = 5f;

    private RandomNumberGenerator _randomGenerator = new();
    private float _shakeStrength;

    public static FollowCamera? Instance { get; private set; }

    public override void _Draw()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            QueueFree();
        }
    }

    public override void _Process(double delta)
    {
        if (_shakeStrength > 0f)
        {
            _shakeStrength = Mathf.Lerp(_shakeStrength, 0f, _shakeStrength * (float)delta);
            Offset = RandomOffset();
        }
    }

    public void Shake() =>
        _shakeStrength = _randomStrength;

    private Vector2 RandomOffset()
    {
        return new Vector2(randomValue(), randomValue());

        float randomValue() => _randomGenerator.RandfRange(_shakeStrength, _shakeStrength);
    }
}