using Godot;

namespace Platformer.Scripts.Constants.Sounds;

public static class CommonSounds
{
    public static readonly AudioStream Blow =
        ResourceLoader.Load<AudioStream>("res://GameAssets/Audio/SFX/Blow.mp3");

    public static readonly AudioStream Hit =
        ResourceLoader.Load<AudioStream>("res://GameAssets/Audio/SFX/Hit.wav");

    public static readonly AudioStream Bubble =
        ResourceLoader.Load<AudioStream>("res://GameAssets/Audio/SFX/Bubble.wav");

    public static readonly AudioStream Coin =
        ResourceLoader.Load<AudioStream>("res://GameAssets/Audio/SFX/Coin.wav");

    public static readonly AudioStream BlockHit =
        ResourceLoader.Load<AudioStream>("res://GameAssets/Audio/SFX/BlockHit.wav");
}