using Godot;
using Platformer.Scripts.Entities;

namespace Platformer.Scripts.Properties;

public partial class ParentCanDamageOnTouch : Node
{
    [Export] public int Damage { get; set; } = 1;
    private Area2D _area;

    public override void _Ready()
    {
        _area = GetParent<Area2D>();
        _area.Ready += () =>
        {
            _area.BodyEntered += body =>
            {
                if (body is Player player)
                {
                    AffectPlayer.Damage(player, Damage);
                }
            };
        };
    }
}