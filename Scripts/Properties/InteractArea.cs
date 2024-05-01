using Godot;

namespace Platformer.Scripts.Properties;

public partial class InteractArea : Area2D
{
    public ParentCanBeInteracted I { get; private set; } = null!;

    public override void _Ready()
    {
        I = GetNode<ParentCanBeInteracted>("ParentCanBeInteracted");
    }
}