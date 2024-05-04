using System.Collections.Generic;
using Platformer.Scripts.Utils.Types;

namespace Platformer.Scripts.Animations;

public enum WormAnim
{
    Idle,
    Attack,
    Move,
    AgroMove
}

public class WormAnimation
{
    public static string Value(WormAnim animation)
    {
        return animation switch
        {
            WormAnim.Idle => "Idle",
            WormAnim.Attack => "Attack",
            WormAnim.Move => "Move",
            WormAnim.AgroMove => "AgroMove",
            _ => "Idle"
        };
    }
}