using Godot;
using Platformer.Scripts.Utils;

namespace Platformer.Scripts.Entities;

public partial class PlayerInput : Node
{
    private Player _player;

    public override void _Ready()
    {
        _player = GetParent<Player>();
    }

    public override void _PhysicsProcess(double delta)
    {
        _player.Direction = Input.GetAxis("MoveLeft", "MoveRight");
        if (InputExt.IsActionHolding("Jump") && _player.IsOnFloor())
        {
            _player.Jumped = true;
        }

        if (InputExt.IsActionHolding("Shoot"))
        {
            _player.Shoot();
        }
    }
}