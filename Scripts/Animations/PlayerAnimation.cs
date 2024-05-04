namespace Platformer.Scripts.Animations;

public enum PlayerAnim
{
    Idle,
    Run,
    Jump,
    Shoot,
    Hit
}

public static class PlayerAnimation
{
    public static string Value(PlayerAnim animation)
    {
        return animation switch
        {
            PlayerAnim.Idle => "Idle",
            PlayerAnim.Run => "Run",
            PlayerAnim.Jump => "Jump",
            PlayerAnim.Shoot => "Shoot",
            PlayerAnim.Hit => "Hit",
            _ => "Idle"
        };
    }
}