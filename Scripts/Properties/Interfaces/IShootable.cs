using Godot;

namespace Platformer.Scripts.Properties.Interfaces;

public interface IShootable
{
    void Construct(Vector2 position, float direction);
}