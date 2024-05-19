using System;
using Godot;

namespace Platformer.Scripts.Properties;

public partial class Health : Node
{
    [Export] private int _current = 1;

    public int Current
    {
        get => _current;
        private set
        {
            _current = value;

            if (_current <= 0)
            {
                _current = 0;
                OnHealthIsZero?.Invoke();
            }

            if (_current >= Max)
            {
                _current = Max;
            }
        }
    }

    [Export] public int Max { get; set; } = 1;

    public Action? OnHealthIsZero { get; set; }
    public Action? OnDamaged { get; set; }

    public void Reduce(int damage)
    {
        Current -= damage;
        OnDamaged?.Invoke();
    }
}