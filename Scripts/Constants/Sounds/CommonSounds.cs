using Godot;

namespace Platformer.Scripts.Constants.Sounds;

public class CommonSounds
{
    public static readonly AudioStream Blow =
        ResourceLoader.Load<AudioStream>("res://GameAssets/Audio/SFX/Blow.mp3");

}