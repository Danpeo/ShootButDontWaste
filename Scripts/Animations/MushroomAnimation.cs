namespace Platformer.Scripts.Animations;

public enum MushroomAnim
{
    Idle,
    Shoot,
    Showup,
    Hide,
    Squash
}

public static class MushroomAnimation
{
    public static string Value(MushroomAnim animation)
    {
        return animation switch
        {
            MushroomAnim.Idle => "Idle",
            MushroomAnim.Shoot => "Shoot",
            MushroomAnim.Showup => "Showup",
            MushroomAnim.Hide => "Hide",
            MushroomAnim.Squash => "Squash",
            _ => "Idle"
        };
    }
}