using System;
using Godot;
using Platformer.Scripts.Weapon;
using Timer = Godot.Timer;

namespace Platformer.Scripts.Entities;

public partial class CanShoot : Node2D
{
    [Export] public string BulletName { get; set; } = "Bullet";
    [Export] public string WeaponSceneName { get; set; }
    [Export] public float Cooldown { get; set; } = 0.25f;

    public Action<int> OnShooted { get; set; }

    private PackedScene _bullet;
    private Timer _shootTimer;
    private bool _canShoot = true;

    public override void _Ready()
    {
        _bullet = GD.Load<PackedScene>($"res://Scenes/Weapon/{BulletName}.tscn");
        _shootTimer = GetNode<Timer>("Timer");
        _shootTimer.WaitTime = Cooldown;
        _shootTimer.Timeout += () => _canShoot = true;
    }

    public void Shoot(float rotation, int ammo)
    {
        if (!_canShoot || ammo <= 0)
            return;

        var bullet = (Bullet)_bullet.Instantiate();
        bullet.Construct(GetNode<Node2D>($"%{WeaponSceneName}").GlobalPosition, rotation);
        GetTree().Root.AddChild(bullet);
        _canShoot = false;
        _shootTimer.Start();
        OnShooted?.Invoke(bullet.BulletCountAsOneShot);
    }
}