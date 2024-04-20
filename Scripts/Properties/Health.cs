using Godot;

namespace Platformer.Scripts.Properties;

public partial class Health : Node
{
    [Export] public int Current { get; set; } = 3;
    [Export] public int Max { get; set; } = 3;
}