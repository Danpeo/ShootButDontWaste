using System.Collections.Generic;
using Platformer.Scripts.Utils.Types;

namespace Platformer.Scripts.Constants.Animations;

/*
public static class PlayerAnimation
{
    public const string Idle = "Idle";
    public const string Run = "Run";
    public const string Jump = "Jump";
    public const string Shoot = "Shoot";
    public const string Hit = "Hit";
}
*/

public enum PlayerAnimationSprite
{
    Armed,
    Unarmed
}

public enum PlayerAnim
{
    Idle,
    Run,
    Jump,
    Shoot,
    Hit
}

public class PlayerAnimation : ValueObject
{
    public string Name { get; }

    public PlayerAnimation(PlayerAnim animation)
    {
        Name = animation switch
        {
            PlayerAnim.Idle => "Idle",
            PlayerAnim.Run => "Run",
            PlayerAnim.Jump => "Jump",
            PlayerAnim.Shoot => "Shoot",
            PlayerAnim.Hit => "Hit",
            _ => "Idle"
        };
    }
    
    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Name;
    }
}