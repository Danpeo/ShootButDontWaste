using Godot;
using Platformer.Scripts.Entities;

namespace Platformer.Scripts.Environment;

public partial class Ladder : Area2D
{
    public override void _Ready()
    {
        BodyEntered += body =>
        {
            /*if (body is Player player)
            {
                player.GoUp();
            }*/
        };

        BodyExited += body =>
        {
            /*if (body is Player player)
            {
                player.GoDown();
            }*/
        };
    }
}