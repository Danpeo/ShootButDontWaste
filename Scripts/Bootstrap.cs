using Godot;

namespace Platformer.Scripts;

public partial class Bootstrap : Node
{
    public override void _Ready()
    {
        Engine.MaxFps = 60;
        GetTree().ChangeSceneToFile("res://Scenes/Levels/Level1.tscn");
    }
}