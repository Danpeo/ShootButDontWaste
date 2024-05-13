using Godot;

namespace Platformer.Scripts.Constants.Sounds;

public static class PlayerSounds
{
    public static readonly AudioStream Jump =
        ResourceLoader.Load<AudioStream>("res://GameAssets/Audio/SFX/PlayerJump.wav");

    public static readonly AudioStream Hit =
        ResourceLoader.Load<AudioStream>("res://GameAssets/Audio/SFX/PlayerHit.wav");
}