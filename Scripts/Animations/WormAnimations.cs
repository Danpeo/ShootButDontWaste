namespace Platformer.Scripts.Animations;

public enum WormAnim
{
    Idle,
    Attack,
    Move,
    AgroMove,
    Hit
}

public static class WormAnimation
{
    public static string Value(WormAnim animation)
    {
        return animation switch
        {
            WormAnim.Idle => "Idle",
            WormAnim.Attack => "Attack",
            WormAnim.Move => "Move",
            WormAnim.AgroMove => "AgroMove",
            WormAnim.Hit => "Hit",
            _ => "Idle"
        };
    }
}