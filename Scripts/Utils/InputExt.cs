using Godot;

namespace Platformer.Scripts.Utils;

public static class InputExt
{
    public static bool IsActionHolding(StringName action) =>
        Input.IsActionPressed(action) && !Input.IsActionJustReleased(action);
}