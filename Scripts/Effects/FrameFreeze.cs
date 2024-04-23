using Godot;

namespace Platformer.Scripts.Effects;

public partial class FrameFreeze : Node
{
    public async void Activate(float timeScale, float duration, float originalTimeScale = 1.0f)
    {
        Engine.TimeScale = timeScale;
        await ToSignal(GetTree().CreateTimer(duration * timeScale), SceneTreeTimer.SignalName.Timeout);
        Engine.TimeScale = originalTimeScale;
    }
}