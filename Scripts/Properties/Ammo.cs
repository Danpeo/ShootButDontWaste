using System;
using Godot;

namespace Platformer.Scripts.Properties;

public partial class Ammo : Node
{
    private int _current = 5;
    public Action? OnAmmoLessThanZero { get; set; }

    [Export]
    public int Current
    {
        get => _current;
        set
        {
            _current = value;

            if (_current < 0)
            {
                OnAmmoLessThanZero?.Invoke();
                _current = 0;
            }

            if (_current >= Max)
            {
                _current = Max;
            }
        }
    }

    [Export] public int Max { get; set; } = 15;

    public Action? OnReducedByShooting { get; set; }
    public Action? OnReducedByDamage { get; set; }
    public Action? OnAdd { get; set; }

    public void ReduceByShooting(int value)
    {
        Reduce(value);
        OnReducedByShooting?.Invoke();
    }

    public void ReduceByDamage(int value)
    {
        Reduce(value);
        OnReducedByDamage?.Invoke();
    }

    public void Add(int value)
    {
        Current += value;
        OnAdd?.Invoke();
    }

    private void Reduce(int value) => Current -= value;
}