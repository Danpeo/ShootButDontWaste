using Godot;
using Platformer.Scripts.Entities;

namespace Platformer.Scripts.Properties;

public partial class ParentCanBeInteracted : Node
{
    public bool CanBeInteracted { get; private set; }
    public Player? Player { get; private set; }
    private Area2D _area = null!;

    public override void _Ready()
    {
        _area = GetParent<Area2D>();
        _area.Ready += () =>
        {
            _area.BodyEntered += body =>
            {
                if (body is Player player)
                {
                    Player = player;
                }
            };

            _area.BodyExited += body =>
            {
                if (body is Player)
                {
                    Player = null;
                }
            };
        };
    }

    public override void _Process(double delta)
    {
        if (Player != null && _area.OverlapsBody(Player))
        {
            CanBeInteracted = true;
        }
        else
        {
            CanBeInteracted = false;
        }
    }
}