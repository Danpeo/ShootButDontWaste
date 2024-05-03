using Godot;

namespace Platformer.Scripts.Properties;

public partial class CanThrow : Node
{
    [Export] public string ThrowableName { get; set; } = "ThrowableBox";
    private PackedScene _interactItemScene = null!;

    public override void _Ready()
    {
        _interactItemScene = GD.Load<PackedScene>($"res://Scenes/Throwables/{ThrowableName}.tscn");
    }

    public void Pickup()
    {
        
    }
}