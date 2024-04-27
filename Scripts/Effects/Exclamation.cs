using Godot;

namespace Platformer.Scripts.Effects;

public partial class Exclamation : AnimatedSprite2D
{
    public override void _Ready()
    {
        PlayDefault();
    }

    private void PlayDefault() => Play("Default");

    public void _Show() => Play("Show");
    public void _Hide()
    {
        Play("Hide");
        PlayDefault();
    }
}