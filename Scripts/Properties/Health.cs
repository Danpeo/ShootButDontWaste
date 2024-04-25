using System;
using Godot;

namespace Platformer.Scripts.Properties;

public partial class Health : Node
{
    private int _current = 1;

    [Export]
    public int Current
    {
        get => _current;
        set
        {
            _current = value;

            if (_current <= 0)
            {
                OnHealthIsZero?.Invoke();
                _current = 0;
            }

            if (_current >= Max)
            {
                _current = Max;
            }
        }
    }

    [Export] public int Max { get; set; } = 1;

    public Action? OnHealthIsZero { get; set; }

    public void Reduce(int damage)
    {
        Current -= damage;
    }
}