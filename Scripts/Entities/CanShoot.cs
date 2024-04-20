using Godot;
using Platformer.Scripts.Weapon;

namespace Platformer.Scripts.Entities;

public partial class CanShoot : Node2D
{
    [Export] public string BulletName { get; set; } = "Bullet";
    [Export] public string WeaponSceneName { get; set; }
    [Export] public float Cooldown { get; set; } = 0.25f;
    private PackedScene _bullet;
    private Timer _shootTimer;
    private bool _canShoot = true;

    public override void _Ready()
    {
        _bullet = GD.Load<PackedScene>($"res://Scenes/Weapon/{BulletName}.tscn");
        _shootTimer = GetNode<Timer>("Timer");
        _shootTimer.WaitTime = Cooldown;
        _shootTimer.Timeout += () => { _canShoot = true; };
    }

    public void Shoot(float rotation)
    {
        if (!_canShoot)
            return;

        var bullet = (Bullet)_bullet.Instantiate();
        bullet.Construct(GetNode<Node2D>($"%{WeaponSceneName}").GlobalPosition, rotation);
        GetTree().Root.AddChild(bullet);
        _canShoot = false;
        _shootTimer.Start();
    }
}